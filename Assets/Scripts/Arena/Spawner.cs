using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pool;
    public int firstNumberToSpawn;
    public int numberToSpawn;
    public int deathEnemiesToSpawn;
    public List<GameObject> enemies;
    public List<GameObject> deathEnemies;
    public float spawnInterval;
    private float nextSpawn;
    private bool restart = true;

    private void Start()
    {
        //SpawnEnemies(firstNumberToSpawn);
        nextSpawn = Time.time + spawnInterval;
    }

    void Update()
    {
        if(Time.time >= nextSpawn)
        {
            SpawnEnemies(numberToSpawn);
            SpawnDeathEnamies(deathEnemiesToSpawn);
            nextSpawn = Time.time + spawnInterval;
        }
        if (restart){
            restart = false;
            SpawnEnemies(firstNumberToSpawn);
        }
    }

    public void SpawnEnemies(float spawnCount)
    {
        if (pool != null)
        {
            int randomEnemy = 0;
            GameObject toSpawn;
            MeshCollider collider = pool.GetComponent<MeshCollider>();

            float screenX, screenY;
            Vector2 position;
            for (int i = 0; i < spawnCount; i++)
            {
                randomEnemy = Random.Range(0, enemies.Count);
                toSpawn = enemies[randomEnemy];

                screenX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                screenY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                position = new Vector2(screenX, screenY);
                GameObject enemy = Instantiate(toSpawn, position, Quaternion.identity);
                enemy.transform.parent = this.gameObject.transform;
            }
        }
    }

    public void SpawnDeathEnamies(float spawnCount)
    {
        if (pool != null)
        {
            int randomEnemy = 0;
            GameObject toSpawn;
            MeshCollider collider = pool.GetComponent<MeshCollider>();

            float screenX, screenY;
            Vector2 position;
            for (int i = 0; i < spawnCount; i++)
            {
                randomEnemy = Random.Range(0, deathEnemies.Count);
                toSpawn = deathEnemies[randomEnemy];

                screenX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                screenY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                position = new Vector2(screenX, screenY);
                GameObject enemy = Instantiate(toSpawn, position, Quaternion.identity);
                enemy.transform.parent = this.gameObject.transform;
            }
        }
    }

    public void RemoveEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        restart = true;
    }
}
