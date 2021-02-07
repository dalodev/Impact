
[System.Serializable]
public class PlayerData
{
    public int level;
    public int coins;
    public int currentExp;
    public int experienceToNextLevel;
    public int highScore;

    public PlayerData(GameController gameController)
    {
        this.coins = gameController.coins;
        this.level = gameController.GetLevel();
        this.currentExp = gameController.GetExperience();
        this.experienceToNextLevel = gameController.GetExperienceToNextLevel();
        this.highScore = gameController.GetHighScore();
    }
}
