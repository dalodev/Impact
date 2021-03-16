
[System.Serializable]
public class PlayerData
{
    public int coins;
    public int highScore;

    public PlayerData(GameController gameController)
    {
        this.coins = gameController.coins;
        this.highScore = gameController.GetHighScore();
    }
}
