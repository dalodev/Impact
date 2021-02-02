[System.Serializable]
public class CustomizeData
{
    public string skin;
    public string trail;

    public CustomizeData(CustomizeManager manager)
    {
        this.skin = manager.skinId;
        this.trail = manager.trailId;
    }
}
