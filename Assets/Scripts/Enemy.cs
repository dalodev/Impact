using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class Enemy : MonoBehaviour
{

    public ShakePreset ShakePreset;
    public GameObject deathEffect;

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
        if(collision.tag == "Player")
        {
            Ball player = collision.GetComponent<Ball>();
            Rigidbody2D rbPlayer = collision.GetComponent<Rigidbody2D>();
            rbPlayer.AddForce(Vector2.up * player.upWardForce, ForceMode2D.Impulse);
            player.canLaunch = true;
            player.timeDragOut = false;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Shaker.ShakeAll(ShakePreset);
            Destroy(gameObject);
            
        }

    }
}
