﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class DeathZone : MonoBehaviour
{

    public GameController gameController;
    public ShakePreset ShakePreset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Shaker.ShakeAll(ShakePreset);
            gameController.PlayerDead();
        }
    }
}
