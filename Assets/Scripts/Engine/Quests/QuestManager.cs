using System;
using System.Collections.Generic;

public class QuestManager
{

    private QuestMetadata questMetaData;

    public enum QUEST_STATES
    {
        UNDISCOVERED,
        ACTIVE,
        COMPLETE,
        FAILED,         // Should a player be able to "fail" a quest?
        UNREACHABLE
    }

    private QUEST_STATES questState = QUEST_STATES.UNDISCOVERED;

    // Current quest node index
    int questNodeIndex = 0;

    private List<QuestNodeManager> questNodeManagers;

    public QuestManager(QuestMetadata questMetaData)
    {
        this.questMetaData = questMetaData;
        questNodeManagers = new List<QuestNodeManager>();
        LoadQuest();
    }

    public void LoadQuest()
    {
        foreach(QuestNodeMetadata questNodeMetaData in questMetaData.questNodes) {
            switch(questNodeMetaData.questNodeType)
            {
                case QuestNodeMetadata.QUEST_NODE_TYPE.CONVERSATION:
                    questNodeManagers.Add(new ConversationQuestNodeManager((ConversationQuestNodeMetadata) questNodeMetaData));
                    break;

                case QuestNodeMetadata.QUEST_NODE_TYPE.ITEM:
                    questNodeManagers.Add(new ItemQuestNodeManager((ItemQuestNodeMetadata) questNodeMetaData));
                    break;
            }
        }
    }

    public QUEST_STATES GetQuestState()
    {
        return questState;
    }

    public int GetQuestNodeIndex()
    {
        return questNodeIndex;
    }

    public override string ToString()
    {
        return questMetaData.questName.ToString() + " : " + questMetaData.questDescription;
    }

    public string GetInfo()
    {
        return string.Format("NAME: {0}\n DESC: {1}\n NODES: {2}",
                             questMetaData.questName,
                             questMetaData.questDescription,
                             String.Join("\n", questNodeManagers));
    }

    public string GetStatus()
    {
        return string.Format("INDEX: {0}\nSTATE: {1}\n",
                             questNodeIndex,
                             questState.ToString());
    }

    public string GetQuestHeaderString()
    {
        return questMetaData.questName;
    }

    public string GetQuestNodeHeaderString()
    {
        return "- " + String.Join("\n- ", questNodeManagers);
    }

    public void Progress()
    {
        questNodeIndex = Math.Min(questNodeIndex + 1, questMetaData.questNodes.Length - 1);
    }

    public void LoadQuestFromSave(QuestSaveData questSaveData)
    {
        questNodeIndex = questSaveData.questNodeIndex;
        questState = questSaveData.questState;
    }

}
