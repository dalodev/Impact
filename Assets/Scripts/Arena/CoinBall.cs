using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class CoinBall : MonoBehaviour
{
    public ShakePreset ShakePreset = null;
    public GameObject deathEffect = null;
    public float upWardForce = 8f;
    public int coins = 1;
    public GameController gameController;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Ball player = collision.gameObject.GetComponent<Ball>();
            Rigidbody2D rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
            rbPlayer.AddForce(rbPlayer.velocity.normalized * upWardForce, ForceMode2D.Impulse);
            player.ResetLaunch(true);
            player.timeDragOut = false;
            if (ShakePreset != null)
            {
                Shaker.ShakeAll(ShakePreset);
            }
            if (deathEffect != null)
            {
                //TODO get coin
                gameController.UpdateCoins(coins);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
