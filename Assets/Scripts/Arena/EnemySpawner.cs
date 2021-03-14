using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject initialEnemies;
    public GameObject playerPool;
    public GameObject pooler;
    ObjectPooler objectPooler;
    public float spawnInterval;
    public int spawnCount;
    private float nextSpawn;
    private bool restart = true;

    private void Start()
    {
        nextSpawn = Time.time + spawnInterval;
        objectPooler = ObjectPooler.instance;
    }

    void FixedUpdate()
    {
        if (Time.time >= nextSpawn)
        {
            SpawnEnemies(100, "Covid");
            nextSpawn = Time.time + spawnInterval;
        }
        if (restart)
        {
            restart = false;
            GameObject initalEnemies = Instantiate(initialEnemies, Vector2.zero, Quaternion.identity);
            initalEnemies.transform.parent = this.gameObject.transform;
            SpawnEnemies(spawnCount, "Enemy");
            SpawnEnemies(200, "EnemyArrow");
            SpawnEnemies(50, "Coin");
            SpawnEnemies(50, "EnemySplit");
            SpawnEnemies(50, "EnemySplitRandom");
        }
    }
    public void SpawnEnemies(int SpawnCount, string tag)
    {
        if (playerPool != null)
        {
            
            MeshCollider collider = playerPool.GetComponent<MeshCollider>();

            float screenX, screenY;
            Vector2 position;
            for (int i = 0; i < SpawnCount; i++)
            {
                screenX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
                screenY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
                position = new Vector3(screenX, screenY, 0);
                GameObject enemy = objectPooler.SpawnFromPool(tag, position, Quaternion.identity);
                if(enemy != null)
                {
                    enemy.transform.parent = this.gameObject.transform;
                }
            }

        }
    }

    public void RemoveEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in pooler.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        objectPooler.CreatePool();
       
        restart = true;
    }
}

public interface IPooledObject
{
    void OnObectSpawn();
}