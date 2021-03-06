[System.Serializable]
public class UpgradesData 
{

    public float[] items;

   public UpgradesData(ShopUpgrades data)
    {
        this.items = new float[data.currentItems.Length+1];
        this.items[this.items.Length -1] = data.GetItemSelected().id;
    }

    public enum Upgrades {
        Speed = 0, 
        TripleLaunch = 1, 
        Experience = 2,
        DragTime = 3,
        AutoBounce = 4,
        LaunchGuide = 5
    };

}
