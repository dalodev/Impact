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
    public AudioClip explosionClip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Ball player = collision.gameObject.GetComponent<Ball>();
            Rigidbody2D rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
            rbPlayer.AddForce(player.launchDirection* upWardForce, ForceMode2D.Impulse);
            player.ResetLaunch(true);
            player.timeDragOut = false;
            if (ShakePreset != null)
            {
                Shaker.ShakeAll(ShakePreset);
            }
            if (deathEffect != null)
            {
                SfxManager.instance.Play(explosionClip);
                player.UpdateScore(points);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            EnemyRadar enemyRadar = collision.gameObject.GetComponent<EnemyRadar>();
            enemyRadar.updateBounceCount();
        }
    }
}
