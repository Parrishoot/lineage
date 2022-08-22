using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestNodeMetadata))]
public class YourScriptableObjClassEditor : Editor
{
    private QuestNodeMetadata targetInfo;
    public void OnEnable()
    {
        if (targetInfo == null)
        {
            targetInfo = target as QuestNodeMetadata;
        }
    }

    public override void OnInspectorGUI()
    {
        // Make a button that calls a static method to open the editor window,
        // passing in the scriptable object information from which the button was pressed
        if (GUILayout.Button("Open Editor Window"))
        {
            Debug.Log("Button!");
        }

        // Remember to display the other GUI from the object if you want to see all its normal properties
        base.OnInspectorGUI();
    }
}