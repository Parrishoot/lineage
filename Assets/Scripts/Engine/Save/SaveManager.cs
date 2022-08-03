using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager: Singleton<SaveManager>
{
    public void SaveQuestState(QuestManager questManager)
    {
        // Formatter to convert to Binary and Back
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/quest.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        QuestSaveData questSaveData = new QuestSaveData(questManager);

        formatter.Serialize(stream, questSaveData);
        stream.Close();
    }

    public QuestSaveData LoadQuest()
    {
        string path = Application.persistentDataPath + "/quest.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            QuestSaveData data = formatter.Deserialize(stream) as QuestSaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return null;
        }
    }
}
