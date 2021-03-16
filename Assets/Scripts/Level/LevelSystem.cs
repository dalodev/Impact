using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelSystem : MonoBehaviour
{
    public Slider levelSlider;
    public TextMeshProUGUI levelText;
    public ParticleSystem partycleSystem;
    public int level;
    public int experience;
    public int experienceToNextLevel;
    public float fillSpeed = 0.5f;
    public float targetProgress = 0;
    public float experienceToUpdateNextLevel = 0;

    private GameController gameController;

    void Update()
    {
        if(levelSlider.value < targetProgress && levelSlider.value < 1)
        {
            levelSlider.value += fillSpeed * Time.deltaTime;
            
            if (!partycleSystem.isPlaying)
            {
                partycleSystem.Play();
            }
        }
        else
        {
            if (targetProgress > 1)
            {
                levelText.text = "Level " + level;
                targetProgress = 0;
                levelSlider.value = 0;
                IncrementProgress((float)experienceToUpdateNextLevel / experienceToNextLevel);
                experienceToUpdateNextLevel = 0;
            }
            else if (levelSlider.value >= 1)
            {
                levelText.text = "Level " + level;
                targetProgress = 0;
                levelSlider.value = 0;
            }
            partycleSystem.Stop();
        }
    }
    
    public void AddExperience(int amount)
    {
        LoadPlayerData();
        experience += amount;
        IncrementProgress(GetExperienceNormalized());
        while (experience >= experienceToNextLevel)
        { 
            //level up
            level += 1;
            experience -= experienceToNextLevel;
            experienceToUpdateNextLevel = experience;
            experienceToNextLevel += experienceToNextLevel / 20;
            SaveSystem.SaveLevelData(this);
        }
        SaveSystem.SaveLevelData(this);
    }

    public void IncrementProgress(float progress)
    {
        targetProgress = levelSlider.value + progress;
    }

    private float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextLevel - levelSlider.value;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetExperience()
    {
        return experience;
    }

    public int GetExperienceToNextLevel()
    {
        return experienceToNextLevel;
    }

    public void LevelUp()
    {
        this.level++;
        SaveSystem.SaveLevelData(this);
    }

    public void AddRevenueExp()
    {
        AddExperience(500);
    }

    public void LoadPlayerData()
    {
        LevelData data = SaveSystem.LoadLevelData();
        if(data != null)
        {
            level = data.level;
            experience = data.currentExp;
            experienceToNextLevel = data.experienceToNextLevel;
        }
        else
        {
            level = 0;
            experience = 0;
            experienceToNextLevel = 1000;
        }
        levelText.text = "Level " + level.ToString();
        levelSlider.value += GetExperienceNormalized();
    }
}
