using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{

    public AudioSource audioSource;

    public static SfxManager instance;

    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

}
