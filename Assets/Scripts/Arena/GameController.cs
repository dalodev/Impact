using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject deathText;
    public GameObject lifeUi;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI levelText;
    public int coins;
    public Ball ball;
    public CustomizeManager customizeManager;
    public LevelSystem levelSystem;
    public Spawner spawner;
    public ArenaTweenManager arenaTweenManager;
    private int highScore;
    private int currentScore = 0;
    private int scoreIncreaseRate = 1;
    private int currentMaxScore = 0;
    private float timer = 0f;
    private bool scoreOverTime = false;

    void Awake()
    {
        //SaveSystem.DeleteData();
        PlayerData data = SaveSystem.LoadPlayerData();
        if (data != null)
        {
            highScore = data.highScore;
        }
        else
        {
            highScore = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)
            && !customizeManager.enabled)
        {
            Restart();
        }
        timer += Time.unscaledDeltaTime;
        /*if(timer > 0.1f && scoreOverTime) {
            currentScore += 5;
            timer = 0;
        }*/
        if (currentScore < currentMaxScore)
        {
            currentScore += scoreIncreaseRate;
        }
        if (!deathText.activeInHierarchy)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void PlayerDead()
    {
        //ActivateScoreOverTime(false);
        //deathText.SetActive(true);
        //lifeUi.SetActive(false);
        arenaTweenManager.ShowDeathPanel(true);
        if(currentScore > highScore)
        {
            highScore = currentScore;
            highScoreText.text = "HighScore: " + currentScore.ToString();
        }
        else
        {
            highScoreText.text = "HighScore: " + highScore.ToString();

        }
        scoreText.text = "Score: " + currentScore.ToString();
        levelSystem.AddExperience(currentScore, this);
        currentMaxScore = 0;
        currentScore = 0;
    }

    public void UpdateScore(int score)
    {
        Debug.Log("Score: " + currentScore);
        currentMaxScore = currentScore + score;
    }

    public void ActivateScoreOverTime(bool activate)
    {
        scoreOverTime = activate;
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
        spawner.RemoveEnemies();
        spawner.gameObject.SetActive(false);
        spawner.gameObject.SetActive(true);
    }

    public int GetLevel()
    {
        return levelSystem.GetLevel();
    }

    public int GetExperience()
    {
        return levelSystem.experience;
    }

    public int GetExperienceToNextLevel()
    {
        return levelSystem.experienceToNextLevel;
    }
}
