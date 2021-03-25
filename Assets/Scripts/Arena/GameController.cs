using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject deathText;
    public GameObject lifeUi;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public int coins = 0;
    public Ball ball;
    public CustomizeManager customizeManager;
    public LevelSystem levelSystem;
    public EnemySpawner enemySpawner;
    public ArenaTweenManager arenaTweenManager;
    public int coinMultiplier = 1;
    public AdMobManager adMobManager;
    public bool loadedFromCloud = false;
    public bool resetData = false;
    private int highScore;
    private int currentScore = 0;
    private int scoreIncreaseRate = 1;
    private int currentMaxScore = 0;
    private int scoreToEvaluate = 0;

    void Awake()
    {
        LoadData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            && ball.enabled)
        {
            Restart();
        }
        
        if (currentScore < currentMaxScore)
        {
            currentScore += scoreIncreaseRate;
        }
        if (!deathText.activeInHierarchy)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void PlayerDead(int xp = 1)
    {
        arenaTweenManager.ShowDeathPanel(true);
        if(currentScore > highScore)
        {
            highScore = currentScore;
            GPGSAuthentication.instance.SubmitScoreToLeaderboard(highScore);
            highScoreText.text = "HighScore: " + currentScore.ToString();
        }
        else
        {
            highScoreText.text = "HighScore: " + highScore.ToString();

        }
        scoreText.text = "Score: " + currentScore.ToString();
        int score = currentMaxScore * xp;
        scoreToEvaluate = currentMaxScore;
        levelSystem.AddExperience(score);
        currentMaxScore = 0;
        currentScore = 0;
        adMobManager.ShowInteresticialAd();
    }

    public void UpdateScore(int score)
    {
        Debug.Log("Score: " + currentScore);
        currentMaxScore = currentScore + score;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    } 

    public int GetHighScore()
    {
        return highScore;
    }

    public void RemoveSpawnerEnemies()
    {
        enemySpawner.RemoveEnemies();
        enemySpawner.gameObject.SetActive(false);
        enemySpawner.gameObject.SetActive(true);
    }

    public void ApplyUpgrades()
    {
        ball.ApplyUpgrades();
        SaveSystem.SavePlayerData(this);
        customizeManager.UpdatePlayerData();
        GameObject.FindObjectOfType<ShopManager>().UpdatePlayerData();
    }

    public void UpdateCoins(int coins)
    {
        this.coins += (coins * coinMultiplier);
        SaveSystem.SavePlayerData(this);
    }

    public void RewardedCoins(int coins)
    {
        this.coins += coins;
        SaveSystem.SavePlayerData(this);
        Shop shop = GameObject.FindObjectOfType<Shop>();
        if(shop != null)
        {
            shop.UpdatePlayerData();
        }
        ShopUpgrades upgrades = GameObject.FindObjectOfType<ShopUpgrades>();
        if(upgrades != null)
        {
            upgrades.UpdatePlayerData();
        }
        CustomizeManager customize = GameObject.FindObjectOfType<CustomizeManager>();
        if(customize != null)
        {
            customize.UpdatePlayerData();
        }
    }

    public void LevelUp()
    {
     
        levelSystem.LevelUp();
    }

    public int GetCurrentScore()
    {
        return scoreToEvaluate;
    }

    public void LoadData()
    {
        if (resetData)
        {
            SaveSystem.DeleteData();
        }
        PlayerData data = SaveSystem.LoadPlayerData();
        if (data != null)
        {
            highScore = data.highScore;
            coins = data.coins;
            loadedFromCloud = data.loadedFromCloud;
        }
        else
        {
            highScore = 0;
            coins = 0;
        }
    }
}
