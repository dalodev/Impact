[System.Serializable]
public class LevelData
{

    public int level = 0;
    public int currentExp;
    public int experienceToNextLevel;

    public LevelData(LevelSystem data)
    {
        this.level = data.level;
        this.currentExp = data.experience;
        this.experienceToNextLevel = data.experienceToNextLevel;
    }
}
