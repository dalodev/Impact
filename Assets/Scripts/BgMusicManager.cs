using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] clips;

    public static BgMusicManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        int clipIndex = Random.Range(0, clips.Length);
        audioSource.clip = clips[clipIndex];
        audioSource.Play();
    }
}
