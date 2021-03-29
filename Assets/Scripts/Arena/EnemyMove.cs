using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class EnemyMove : MonoBehaviour
{
    public Vector3 targetPosition;
    
    void Start()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
        if(transform.position == targetPosition)
        {
            this.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
