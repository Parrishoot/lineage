using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager: MonoBehaviour
{

    public GameObject questUIPrefab;
    public GameObject questNodeUIPrefab;

    private QuestMetadata questMetaData;

    private QuestUIController questUIController;

    // TODO: Use this somehow
    private bool active = false;

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

    public void Init(QuestMetadata questMetaData)
    {
        this.questMetaData = questMetaData;
        questNodeManagers = new List<QuestNodeManager>();
        questUIController = GameObject.Instantiate(questUIPrefab, this.transform).GetComponent<QuestUIController>();
        questUIController.SetHeaderText(questMetaData.questName);
        InitQuest();

        Deactivate();
    }

    public void Update()
    {
        // TODO: Maybe update this and call it somewhere else
        if(GetActiveQuestNodeManager().IsFinished())
        {
            Progress();
        }
    }

    public void InitQuest()
    {
        foreach(QuestNodeMetadata questNodeMetaData in questMetaData.questNodes) {
            AddQuestNode(questNodeMetaData);
        }
    }

    public void Activate()
    {
        active = true;
        questUIController.Activate();
        GetActiveQuestNodeManager().Activate();
    }

    public void Deactivate()
    {
        active = false;
        questUIController.Deactivate();
    }

    public GameObject GetQuestUIElement()
    {
        return questUIController.gameObject;
    }

    private void AddQuestNode(QuestNodeMetadata questNodeMetadata)
    {
        GameObject questNodeUIElement = GameObject.Instantiate(questNodeUIPrefab, this.transform);
        questUIController.AddQuestNode(questNodeUIElement);

        QuestNodeUIController questNodeUIController = questNodeUIElement.GetComponent<QuestNodeUIController>();

        switch (questNodeMetadata.questNodeType)
        {
            case QuestNodeMetadata.QUEST_NODE_TYPE.CONVERSATION:
                questNodeManagers.Add(new ConversationQuestNodeManager((ConversationQuestNodeMetadata) questNodeMetadata, questNodeUIController));
                break;

            case QuestNodeMetadata.QUEST_NODE_TYPE.ITEM:
                questNodeManagers.Add(new ItemQuestNodeManager((ItemQuestNodeMetadata)questNodeMetadata, questNodeUIController));
                break;
        }
    }

    public void InitQuestOnSceneTransition()
    {
        GetActiveQuestNodeManager().InitQuestOnSceneTransition();
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
        questNodeIndex++;

        if(questNodeIndex == questNodeManagers.Count)
        {
            Debug.Log("Congrats on completing the quest!");
        }
        else
        {
            GetActiveQuestNodeManager().Activate();
        }
    }

    public QuestNodeManager GetActiveQuestNodeManager()
    {
        return questNodeManagers[questNodeIndex];
    }

    public void LoadQuest(QuestSaveData questSaveData)
    {
        questNodeIndex = questSaveData.questNodeIndex;
        questState = questSaveData.questState;
    }

    public QuestSaveData GetSaveData() {
        return new QuestSaveData(this);
    }

}
