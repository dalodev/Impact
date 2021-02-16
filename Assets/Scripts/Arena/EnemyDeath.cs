using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class EnemyDeath : MonoBehaviour
{
    public ShakePreset ShakePreset = null;
    public GameObject deathEffect = null;
    public float upWardForce = 8f;
    public int points = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Ball player = collision.gameObject.GetComponent<Ball>();
            
           
            if (ShakePreset != null)
            {
                Shaker.ShakeAll(ShakePreset);
            }
            if (deathEffect != null)
            {
                player.UpdateScore(points);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            player.PlayerDeath();
        }
    }
}
