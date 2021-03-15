using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class DeathZone : MonoBehaviour
{
    public GameController gameController;
    public ShakePreset ShakePreset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Death");
            collision.gameObject.SetActive(false);
            Shaker.ShakeAll(ShakePreset);
            Ball player = collision.gameObject.GetComponent<Ball>();

            if (player.deathEffect != null)
            {
                Instantiate(player.deathEffect, collision.gameObject.transform.position, player.deathEffect.transform.rotation);
            }
            gameController.PlayerDead(collision.gameObject.GetComponent<Ball>().xp);
        }
    }
}
