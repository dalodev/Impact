using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyRadar : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform closestEnemy;
    public bool enemyContact;
    public int bounceCount = 100;
    public float bounceRate = 3;
    public float force = 2f;
    public TextMeshProUGUI bounceRateText;

    private Transform player;
    private Rigidbody2D rbPlayer;
    private int currentBounceCount = 0;
    private float currentBounceRate;

    // Start is called before the first frame update
    void Start()
    {
        closestEnemy = null;
        enemyContact = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rbPlayer = player.GetComponent<Rigidbody2D>();
        bounceRateText.text = "x" + bounceRate;
        currentBounceRate = bounceRate;
    }

    // Update is called once per frame
    void Update()
    {
        closestEnemy = getClosestEnemy();
        if (player != null && closestEnemy != null)
        {
            if (currentBounceCount > 0)
            {
                Vector3 f = closestEnemy.position - transform.position;
                f = f.normalized;
                f *= force;
                rbPlayer.AddForce(f, ForceMode2D.Impulse);
            }
        }
    }

    public void updateBounceCount()
    {
        this.currentBounceCount -= 1;
    }

    public void ResetAutoBounce()
    {
        this.currentBounceCount = 0;
    }

    public void AutoBounce()
    {
        if(currentBounceRate > 0)
        {
            currentBounceRate -= 1;
            this.currentBounceCount = bounceCount;
        }
        bounceRateText.text = "x" + Mathf.Clamp(currentBounceRate, 0, bounceRate);
    }

    public void updateBounceRate()
    {
        currentBounceRate = bounceRate;
        bounceRateText.text = "x" + Mathf.Clamp(currentBounceRate, 0, bounceRate);
    }

    public Transform getClosestEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach(GameObject go in enemies)
        {
            float currentDistance;
            currentDistance = Vector2.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }
}
