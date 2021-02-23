[System.Serializable]
public class UpgradesData 
{

    public float[] items;

   public UpgradesData(ShopUpgrades data)
    {
        this.items = new float[data.items.Length];
        this.items[data.currentItems.Length+1] = data.GetItemSelected().id;
    }

    public enum Upgrades {
        Speed = 1, 
        TripleLaunch = 2, 
        Experience = 3,
        DragTime = 4,
        AutoBounce = 5
    };

}
