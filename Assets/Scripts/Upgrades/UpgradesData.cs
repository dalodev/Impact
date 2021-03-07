[System.Serializable]
public class UpgradesData 
{

    public int[] items;

   public UpgradesData(ShopUpgrades data)
    {
        this.items = new int[data.myItems.Count];
        for (int i = 0; i < data.myItems.Count; i++)
        {
            this.items[i] = data.myItems[i];
        }
    }

    public enum Upgrades {
        Speed = 1, 
        TripleLaunch = 2, 
        Experience = 3,
        DragTime = 4,
        AutoBounce = 5,
        LaunchGuide = 6,
        LevelUp = 7 //TODO reset every day at 23:59
    };

} 
