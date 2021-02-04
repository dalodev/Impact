
[System.Serializable]
public class PlayerData
{
    public int level;
    public int coins;

    public PlayerData(GameController gameController)
    {
        this.coins = gameController.level;
        this.level = gameController.coins;
    }
}
