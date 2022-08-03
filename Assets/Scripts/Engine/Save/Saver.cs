using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class Saver<T>
{

    private string filePath;

    public Saver(string filePath)
    {
        this.filePath = filePath;
    }

    public void Save(T saveData)
    {
        // Formatter to convert to Binary and Back
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + filePath;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public T Load()
    {
        string path = Application.persistentDataPath + filePath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            T data = (T) formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found at " + path);
            return default(T);
        }
    }

}
