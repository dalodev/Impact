using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer effectsAudioMixer;
    public Slider volumeSlider;
    public Slider volumeEffectsSlider;
    public TMP_Dropdown dropDownGraphics;

    void Start()
    {
        volumeSlider.value = 0;
        volumeEffectsSlider.value = 0;
        SettingsData data = SaveSystem.LoadSettingsData();
        if(data != null)
        {
            volumeSlider.value = data.volumeValue;
            volumeEffectsSlider.value = data.effectsVolume;
        }
        dropDownGraphics.value = 4;
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

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
