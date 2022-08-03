using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestSaveData
{
    public QuestManager.QUEST_STATES questState;
    public int questNodeIndex;

    public QuestSaveData(QuestManager questManager)
    {
        questState = questManager.GetQuestState();
        questNodeIndex = questManager.GetQuestNodeIndex();
    }
}
