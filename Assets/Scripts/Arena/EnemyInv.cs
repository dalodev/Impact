using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class EnemyInv : MonoBehaviour
{
    public ShakePreset ShakePreset = null;
    public GameObject deathEffect = null;
    public float upWardForce = 8f;
    public float speed = 4f;
    public int points = 0;
    private Vector3 direction = Vector3.right;
    private Vector3 rotationDirection = Vector3.right;
    private Vector2 intialPosition;
    private Vector3 rotation;

    private void Start()
    {
        int rotationDirection = Random.Range(0, 2);
        if(rotationDirection == 0)
        {
            rotation = Vector3.forward;
        }
        else
        {
            rotation = -Vector3.forward;
        }
        intialPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * speed);
        Vector3 forward = transform.TransformDirection(direction);
        rotationDirection = forward;
        Vector3 position = transform.position;
        Debug.DrawLine(position, position + direction, Color.red);
        Debug.DrawLine(position, position + forward, Color.green);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Ball player = collision.gameObject.GetComponent<Ball>();
            Rigidbody2D rbPlayer = collision.gameObject.GetComponent<Rigidbody2D>();
            rbPlayer.velocity = Vector3.zero;
            rbPlayer.AddForce(rotationDirection * upWardForce, ForceMode2D.Impulse);
            player.canLaunch = true;
            player.timeDragOut = false;
            if (ShakePreset != null)
            {
                Shaker.ShakeAll(ShakePreset);
            }
            if (deathEffect != null)
            {
                player.UpdateScore(points);
                Instantiate(deathEffect, intialPosition, Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }
}
