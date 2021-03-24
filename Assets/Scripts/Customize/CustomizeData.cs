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

    public CustomizeData(string skin, string trail)
    {
        this.skin = skin;
        this.trail = trail;
    }

    public enum CustomizeIndex
    {
        CUSTOMIZE_SKIN = 0,
        CUSTOMIZE_TRAIL = 1
    }
}
