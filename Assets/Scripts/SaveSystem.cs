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
        GPGSAuthentication.instance.OpenSaveToCloud(true);
    }

    public static void SaveCustomize(string skin, string trail)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/customize.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        CustomizeData data = new CustomizeData(skin, trail);
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
        GPGSAuthentication.instance.OpenSaveToCloud(true);
    }

    public static void SavePlayerData(int coins, int highScore, bool loadedFromCloud)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(coins, highScore, loadedFromCloud);
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
        GPGSAuthentication.instance.OpenSaveToCloud(true);
    }

    public static void SaveUpgrades(int[] items, int levelUpMultiplier)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/upgrades.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        UpgradesData data = new UpgradesData(items, levelUpMultiplier);
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
            Debug.Log("upgrades data " + data.items);

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveLevelData(LevelSystem levelSystem)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelSystem);
        formatter.Serialize(stream, data);
        stream.Close();
        GPGSAuthentication.instance.OpenSaveToCloud(true);
    }

    public static void SaveLevelData(int level, int experience, int expToNextLevel)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(level, experience, expToNextLevel);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevelData()
    {
        string path = Application.persistentDataPath + "/level.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveSettingsData(Settings settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settings);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsData LoadSettingsData()
    {
        string path = Application.persistentDataPath + "/settings.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SettingsData data = formatter.Deserialize(stream) as SettingsData;
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
