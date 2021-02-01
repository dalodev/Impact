using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveCustomize(CustomizeManager customize)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/customize.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        CustomizeData data = new CustomizeData(customize);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CustomizeData LoadCustomize()
    {
        string path = Application.persistentDataPath + "/customize.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CustomizeData data = formatter.Deserialize(stream) as CustomizeData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    
}
