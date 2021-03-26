
[System.Serializable]
public class PlayerData
{
    public int coins = 0;
    public int highScore = 0;
    public bool loadedFromCloud = false;
    public string appVersion;

    public PlayerData(GameController gameController)
    {
        this.coins = gameController.coins;
        this.highScore = gameController.GetHighScore();
        this.loadedFromCloud = gameController.loadedFromCloud;
        this.appVersion = gameController.appVersion;
    }

    public PlayerData(int coins, int highScore, bool loadedFromCloud, string appVersion)
    {
        this.coins = coins;
        this.highScore = highScore;
        this.loadedFromCloud = loadedFromCloud;
        this.appVersion = appVersion;
    }

    public enum PlayerIndex
    {
        PLAYER_COINS = 2,
        PLAYER_HIGHSCORE = 3
    }
}
