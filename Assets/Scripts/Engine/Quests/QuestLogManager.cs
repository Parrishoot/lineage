using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogManager : MonoBehaviour
{
    public List<QuestMetadata> questList;

    public int activeQuestIndex = 0;

    public QuestTextController questTextController;

    private List<QuestManager> questManagers = new List<QuestManager>();
    private QuestManager activeQuest;

    private void Start()
    {
        foreach(QuestMetadata questMetaData in questList)
        {
            questManagers.Add(new QuestManager(questMetaData));
        }

        // TODO: UPDATE THIS
        activeQuest = questManagers[activeQuestIndex];
    }

    public void Update()
    {
        questTextController.SetText(activeQuest.GetQuestHeaderString(), activeQuest.GetQuestNodeHeaderString());
    }
}
