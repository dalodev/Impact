using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneMovement : MonoBehaviour
{

    public Transform target;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = new Vector2(target.position.x, position.y);
        }
    }
}
