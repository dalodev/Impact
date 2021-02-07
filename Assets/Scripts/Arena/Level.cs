using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    public Slider levelSlider;
    public float fillSpeed = 0.5f;
    public ParticleSystem particles;
    public float maxExp;
    public TextMeshProUGUI levelText;
    private float targetProgress = 0;


    private int level = 0;
    private int experience = 0;
    private int experienceToNextLevel = 100;

    // Update is called once per frame
    void Update()
    {
       
        if(levelSlider.value < targetProgress)
        {
            levelSlider.value += fillSpeed * Time.deltaTime;
            if (!particles.isPlaying)
            {
                particles.Play();
            }
            if (levelSlider.value == maxExp / 100)
            {
                levelSlider.value = 0;
                levelText.text = "Level " + level;
                targetProgress = 0;
            }
        }
        else
        {
            particles.Stop();
        }
    }

    public void AddExperience(int xp)
    {
        experience += xp;
        if(experience >= experienceToNextLevel)
        {
            //enoughexperience to level up
            level++;
            experience -= experienceToNextLevel;
        }

        targetProgress = levelSlider.value + xp ;
    }

    public float GetExperienceNormalized()
    {
        return (float)experience / experienceToNextLevel;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetExperience()
    {
        return experience;
    }

    public void SetExperience(int experience)
    {
        this.experience = experience;
    }
}
