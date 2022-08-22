using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestLogManager))]
public class QuestLogManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        QuestLogManager questLogManager = target as QuestLogManager;

        GUILayout.Space(5f);

        if (GUILayout.Button("Save"))
        {
            questLogManager.SaveActiveQuest();
        }

        if (GUILayout.Button("Load"))
        {
            questLogManager.LoadActiveQuest();
        }

        if (GUILayout.Button("Progress"))
        {
            questLogManager.ProgressActiveQuest();
        }
    }
}