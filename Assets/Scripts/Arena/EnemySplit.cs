using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class EnemySplit : MonoBehaviour
{

    public ShakePreset ShakePreset = null;
    public GameObject deathEffect = null;
    public GameObject enemyPrefab;
    public GameObject[] enemies;
    public float upWardForce = 8f;
    public int points = 0;
    public Transform[] targetPositon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Ball player = collision.gameObject.GetComponent<Ball>();
            Rigidbody2D rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
            rbPlayer.AddForce(-player.launchDirection * upWardForce, ForceMode2D.Impulse);
            player.ResetLaunch(true);
            player.timeDragOut = false;
            if(enemyPrefab != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject enemyObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    EnemyMove enemy = enemyObject.GetComponent<EnemyMove>();
                    if (enemy != null)
                    {
                        enemy.enabled = true;
                        enemy.targetPosition = targetPositon[i].position;
                    }
                }
            }
            if(enemies != null && enemies.Length > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject randomEnemy = enemies[Random.Range(0, enemies.Length)];
                    GameObject enemyObject = Instantiate(randomEnemy, transform.position, Quaternion.identity);
                    EnemyMove enemy = enemyObject.GetComponent<EnemyMove>();
                    if(enemy != null)
                    {
                        enemy.enabled = true;
                        enemy.targetPosition = targetPositon[i].position;
                    }
                }
            }
            
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
