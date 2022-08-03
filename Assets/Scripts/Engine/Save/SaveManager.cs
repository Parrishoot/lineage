using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : Singleton<SaveManager>
{

    // CONSTANTS FOR SAVE FILE INFO
    public const string SAVE_PATH = "/";
    public const string QUEST_FILE_NAME = "quests.save";

    private Saver<QuestSaveData> questSaver = new Saver<QuestSaveData>(SAVE_PATH + QUEST_FILE_NAME);

    public void SaveQuest(QuestManager questManager)
    {
        questSaver.Save(questManager.GetSaveData());
    }

    public QuestSaveData LoadQuest()
    {
        return questSaver.Load();
    }
}
