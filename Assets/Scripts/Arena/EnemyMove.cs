using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class EnemyMove : MonoBehaviour
{
    public Vector3 targetPosition;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
    }
}
