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
            collision.gameObject.SetActive(false);
            Shaker.ShakeAll(ShakePreset);
            gameController.PlayerDead(collision.gameObject.GetComponent<Ball>().xp);
        }
    }
}
