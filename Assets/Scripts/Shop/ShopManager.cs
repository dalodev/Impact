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
            this.level = data.level;
            this.coins = data.coins;
        }
        else
        {
            this.level = 0;
            this.coins = 0;
        }
    }

    public int GetLevel()
    {
        return this.level;
    }
}
