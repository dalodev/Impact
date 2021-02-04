using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Slider levelSlider;
    public float fillSpeed = 0.5f;
    public ParticleSystem particles;
    private float targetProgress = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

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
        }
        else
        {
            particles.Stop();
        }
    }

    public void IncrementXp(float xp)
    {
        targetProgress = levelSlider.value + xp / 100;
    }
}
