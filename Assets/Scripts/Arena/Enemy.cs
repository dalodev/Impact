using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class Enemy : MonoBehaviour
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
            Rigidbody2D rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
            rbPlayer.AddForce(Vector2.up * upWardForce, ForceMode2D.Impulse);
            player.canLaunch = true;
            player.timeDragOut = false;
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

        }
    }
}
