using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer effectsAudioMixer;
    public Slider volumeSlider;
    public Slider volumeEffectsSlider;
    public TMP_Dropdown dropDownLAnguage;
    public TextMeshProUGUI languageLabel;
    public int languageIndex;

    void Start()
    {
        volumeSlider.value = 0;
        volumeEffectsSlider.value = 0;
        languageIndex = 0;
        SettingsData data = SaveSystem.LoadSettingsData();
        if(data != null)
        {
            volumeSlider.value = data.volumeValue;
            volumeEffectsSlider.value = data.effectsVolume;
            languageIndex = data.languageIndex;
        }
        dropDownLAnguage.value = languageIndex;
        StartCoroutine(LoadLanguage());
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        SaveSystem.SaveSettingsData(this);
    }

    public void SetEffectsVolume(float volume)
    {
        effectsAudioMixer.SetFloat("effectsVolume", volume);
        SaveSystem.SaveSettingsData(this);
    }

    public void SetLanguage(int languageIndex)
    {
        this.languageIndex = languageIndex;
        StartCoroutine(LoadLanguage());
        SaveSystem.SaveSettingsData(this);
    }

    public IEnumerator LoadLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
    }
}
