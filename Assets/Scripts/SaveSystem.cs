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
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void SavePlayerData(GameController gameController)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(gameController);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveUpgrades(ShopUpgrades upgradesData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/upgrades.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        UpgradesData data = new UpgradesData(upgradesData);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static UpgradesData LoadUpgrades()
    {
        string path = Application.persistentDataPath + "/upgrades.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UpgradesData data = formatter.Deserialize(stream) as UpgradesData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void DeleteData()
    {
        DirectoryInfo dataDir = new DirectoryInfo(Application.persistentDataPath);
        dataDir.Delete(true);
    }


}
