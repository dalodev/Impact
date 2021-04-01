using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public double coins;
    public int level;
    public GameController gameController;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        LoadData();
    }

    public void LoadData()
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

    public void BuyCoin(CoinType coinType)
    {
        Debug.Log("BuyCoin" + (int)coinType);
        gameController.RewardedCoins((int)coinType);
    }

    public enum CoinType
    {
        Coin100 = 100, 
        Coin220 = 220,
        Coin550 = 550,
        Coin1200 = 1200, 
        Coin2500 = 2500, 
        Coin5200 = 5200
    }
}
