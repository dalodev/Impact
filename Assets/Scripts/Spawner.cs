using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject space;
    public int firstNumberToSpawn;
    public int numberToSpawn;
    public List<GameObject> enemies;
    public float spawnInterval;
    private float nextSpawn;


    private void Start()
    {
        spawnEnemies(firstNumberToSpawn);
        nextSpawn = Time.time + spawnInterval;
    }

    void Update()
    {
        if(Time.time >= nextSpawn)
        {
            spawnEnemies(numberToSpawn);
            nextSpawn = Time.time + spawnInterval;
        }
    }

    public void spawnEnemies(float spawnCount)
    {
        int randomEnemy = 0;
        GameObject toSpawn;
        MeshCollider collider = space.GetComponent<MeshCollider>();

        float screenX, screenY;
        Vector2 position;
        for(int i = 0; i < spawnCount; i++)
        {
            randomEnemy = Random.Range(0, enemies.Count);
            toSpawn = enemies[randomEnemy];

            screenX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
            screenY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
            position = new Vector2(screenX, screenY);
            Instantiate(toSpawn, position, Quaternion.identity);
        }
    }
}
