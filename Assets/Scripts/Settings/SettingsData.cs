[System.Serializable]
public class SettingsData 
{
    public int volumeValue;
    public int effectsVolume;
    public int languageIndex;

    public SettingsData(Settings settings)
    {
        this.volumeValue = (int)settings.volumeSlider.value;
        this.effectsVolume = (int)settings.volumeEffectsSlider.value;
        this.languageIndex = settings.languageIndex;
    }
}
