using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestLogManager : Singleton<QuestLogManager>
{
    public List<QuestMetadata> questList;

    public int activeQuestIndex = 0;

    public GameObject questManagerPrefab;

    public QuestLogUIController questLogUIController;

    private List<QuestManager> questManagers = new List<QuestManager>();
    private QuestManager activeQuest;

    void Start()
    {
        foreach(QuestMetadata questMetaData in questList)
        {
            QuestManager questManager = GameObject.Instantiate(questManagerPrefab, this.transform).GetComponent<QuestManager>();

            questManager.Init(questMetaData);
            questManagers.Add(questManager);

            questLogUIController.AddQuestUIElement(questManager.GetQuestUIElement());

            questManager.Deactivate();
        }

        // TODO: UPDATE THIS
        // ActivateQuest(activeQuestIndex);
    }

    public void InitActiveQuestOnSceneTransition()
    {
        GetActiveQuestManager().InitQuestOnSceneTransition();
    }

    public void ActivateQuest(int questIndex)
    {
        if(activeQuest != null)
        {
            activeQuest.Deactivate();
        }

        activeQuest = questManagers[questIndex];
        activeQuest.SetActive();
    }

    public QuestManager GetQuestManager(int index)
    {
        return questManagers[index];
    }

    public QuestManager GetActiveQuestManager()
    {
        return questManagers[activeQuestIndex];
    }

    public void SaveActiveQuest()
    {
        SaveManager.GetInstance().SaveQuest(GetActiveQuestManager());
    }

    public void LoadActiveQuest()
    {
        GetActiveQuestManager().LoadQuest(SaveManager.GetInstance().LoadQuest());
    }

    public void ProgressActiveQuest()
    {
        GetActiveQuestManager().Progress();
        Debug.Log(GetActiveQuestManager().GetStatus());
    }
}
