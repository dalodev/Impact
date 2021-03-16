using UnityEngine;

public class ShopManager : MonoBehaviour
{ 
    public double coins;
    public int level;

    void Start()
    {
        UpdatePlayerData();
    }

    public void UpdatePlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        if (data != null)
        {
            this.coins = data.coins;
        }
        else
        {
            this.coins = 0;
        }
        LevelData levelData = SaveSystem.LoadLevelData();
        if (levelData != null)
        {
            this.level = levelData.level;
        }
        else
        {
            this.level = 0;
        }
    }

    public int GetLevel()
    {
        return this.level;
    }
}
