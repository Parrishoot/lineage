using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

// Original Source : https://stackoverflow.com/questions/56180821/how-can-i-use-reorderablelist-with-a-list-in-the-inspector-and-adding-new-empty
[CustomEditor(typeof(QuestMetadata))]
[CanEditMultipleObjects]
public class CardDataEditor : Editor
{
    private ReorderableList questNodeList;

    private SerializedProperty questNodesProp;

    private struct QuestNodeCreationParams
    {
        public string Path;
    }

    public void OnEnable()
    {

        questNodesProp = serializedObject.FindProperty("questNodes");

        questNodeList = new ReorderableList(
                serializedObject,
                questNodesProp,
                draggable: true,
                displayHeader: true,
                displayAddButton: true,
                displayRemoveButton: true);

        questNodeList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Quest Nodes");
        };

        questNodeList.onRemoveCallback = (ReorderableList l) => {

            var element = l.serializedProperty.GetArrayElementAtIndex(l.index);
            var obj = element.objectReferenceValue;

            AssetDatabase.RemoveObjectFromAsset(obj);

            DestroyImmediate(obj, true);
            l.serializedProperty.DeleteArrayElementAtIndex(l.index);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        };

        questNodeList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            SerializedProperty element = questNodesProp.GetArrayElementAtIndex(index);

            rect.width -= 10;
            rect.height = EditorGUIUtility.singleLineHeight;

            if (element.objectReferenceValue == null)
            {
                return;
            }
            string label = element.objectReferenceValue.name;
            EditorGUI.LabelField(rect, label, EditorStyles.boldLabel);

            // Convert this element's data to a SerializedObject so we can iterate
            // through each SerializedProperty and render a PropertyField.
            SerializedObject nestedObject = new SerializedObject(element.objectReferenceValue);

            // Loop over all properties and render them
            SerializedProperty prop = nestedObject.GetIterator();
            float y = rect.y;
            while (prop.NextVisible(true))
            {

                if (prop.name == "m_Script")
                {
                    continue;
                }

                rect.y += EditorGUI.GetPropertyHeight(prop);
                EditorGUI.PropertyField(rect, prop);
                rect.y += 2;

                // GUILayout.Space(100);

            }

            nestedObject.ApplyModifiedProperties();

            // Mark edits for saving
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }

        };

        questNodeList.elementHeightCallback = (int index) => {
            float baseProp = EditorGUI.GetPropertyHeight(
                questNodeList.serializedProperty.GetArrayElementAtIndex(index), true);

            float additionalProps = 0;
            SerializedProperty element = questNodesProp.GetArrayElementAtIndex(index);
            if (element.objectReferenceValue != null)
            {
                SerializedObject ability = new SerializedObject(element.objectReferenceValue);
                SerializedProperty prop = ability.GetIterator();
                while (prop.NextVisible(true))
                {
                    // XXX: This logic stays in sync with loop in drawElementCallback.
                    if (prop.name == "m_Script")
                    {
                        continue;
                    }
                    additionalProps += EditorGUIUtility.singleLineHeight;
                }
            }

            float spacingBetweenElements = EditorGUIUtility.singleLineHeight / 2;

            return baseProp + spacingBetweenElements + additionalProps;
        };

        questNodeList.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {
            var menu = new GenericMenu();
            var guids = AssetDatabase.FindAssets("", new[] { "Assets/Scripts/Engine/Quests/QuestNodes/ObjectDefinitions" });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var type = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
                if (type.name == "QuestNode")
                {
                    continue;
                }

                menu.AddItem(
                    new GUIContent(Path.GetFileNameWithoutExtension(path)),
                    false,
                    addClickHandler,
                    new QuestNodeCreationParams() { Path = path });
            }
            menu.ShowAsContext();
        };
    }

    private void addClickHandler(object dataObj)
    {
        // Make room in list
        var data = (QuestNodeCreationParams) dataObj;
        var index = questNodeList.serializedProperty.arraySize;
        questNodeList.serializedProperty.arraySize++;
        var element = questNodeList.serializedProperty.GetArrayElementAtIndex(index);

        // Create the new Ability
        var type = AssetDatabase.LoadAssetAtPath(data.Path, typeof(UnityEngine.Object));
        var newQuestNode = ScriptableObject.CreateInstance(type.name);
        newQuestNode.name = type.name;

        // Add it to CardData
        var questMetaData = (QuestMetadata) target;
        AssetDatabase.AddObjectToAsset(newQuestNode, questMetaData);
        AssetDatabase.SaveAssets();
        element.objectReferenceValue = newQuestNode;
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        questNodeList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}