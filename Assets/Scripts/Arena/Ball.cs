using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public LayerMask groundLayer;

    [Header("Physics")]
    public float speed = 10f;
    public float upWardForce = 10f;
    public float maxSpeed = 5f;

    [Header("Launch movment")]
    public float launchSpeed = 15f;
    public LineRenderer line;
    public float maxDrag = 5f;
    public int dragCount = 0;
    public int maxDragCount = 1;
    public Vector2 launchDirection;
    public int launchGuide = 650;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;

    public TimeManager timeManager;
    public BallState state = BallState.NONE;

    [Header("Stats")]
    public float dragTimer = 10f;
    public int xp;

    [Header("Game")]
    public GameController gameCotroller;

    public bool timeDragOut = false;

    public Camera myCamera;
    public GameObject deathEffect;
    Vector3 endPoint;

    public bool canLaunch = true;

    [Header("Customize")]
    public TrailRenderer trail; 

    void Awake()
    {
        ApplyUpgrades();
        enabled = false;
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude > maxDrag)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    public bool IsOnGround()
    {
        return Physics2D.RaycastAll(transform.position, Vector2.down, groundLength, groundLayer).Length > 0;
    }

    public void StartDrag()
    {
        timeManager.DoSlowMotion();
    }

    public void Dragging()
    {
        state = BallState.DRAGGING;
        Vector3 currentPoint = myCamera.ScreenToWorldPoint(Input.mousePosition);
        currentPoint.z = 0f;
        DrawLine(transform.position, currentPoint);
    }

    public void Launch()
    {
        //gameCotroller.ActivateScoreOverTime(true);
        if(dragCount < maxDragCount)
        {
            dragCount++;
            canLaunch = true;
        }
        else
        { 
            dragCount = 0;
            canLaunch = false;
        }
        state = BallState.LAUNCH;
        if (!timeDragOut)
        {
            rb.velocity = Vector2.zero;
            timeManager.BackToNormal();
            endPoint = myCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 launchForce = GetLaunchDirection(endPoint);
            Vector2 clampForce = Vector2.ClampMagnitude(launchForce, maxDrag);
            launchDirection = clampForce;
            rb.AddForce(clampForce * launchSpeed, ForceMode2D.Impulse);
        }
        EndLine();
    }

    void DrawLine(Vector3 startPoint, Vector3 endPoint)
    {
        line.enabled = true;
        Vector2 vel = launchSpeed * Vector2.ClampMagnitude(GetLaunchDirection(endPoint), maxDrag);
        if (vel.magnitude < 1) return;

        float x = transform.position.x;
        float y = transform.position.y;
        float yVel = vel.y;
        float xVel = vel.x;
        float gravity = 9.81f;

        int iterations = launchGuide / (int) vel.magnitude;
        float iterationsDist = 1f;
        line.positionCount = iterations;
        line.startColor = Color.white;
        for (int i = 0; i < iterations; i++)
        {
            line.SetPosition(i, new Vector2(x, y));
            yVel -= gravity * 0.02f * iterationsDist;
            y += yVel * 0.02f * iterationsDist;
            x += xVel * 0.02f * iterationsDist;
        }
    }

    private Vector2 GetLaunchDirection(Vector3 mousePosition)
    {
        return transform.position - mousePosition;
    }

    void EndLine()
    {
        line.enabled = false;
    }

    public void UpdateScore(int score)
    {
        gameCotroller.UpdateScore(score);
    }

    public void PlayerDeath()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        gameCotroller.PlayerDead(xp);
        gameObject.SetActive(false);
    }

    public void ResetLaunch(bool canLaunch) {
        this.canLaunch = canLaunch;
        this.dragCount = 0;
    }

   public enum BallState
    {
        NONE,
        DRAGGING,
        LAUNCH
    }


    public void ApplyUpgrades()
    {
        UpgradesData data = SaveSystem.LoadUpgrades();
        if(data != null)
        {
            for(int i = 0; i < data.items.Length; i++)
            {
                switch ((int)data.items[i])
                {
                    case (int)UpgradesData.Upgrades.Speed:
                        this.launchSpeed = 8;
                        break;
                    case (int)UpgradesData.Upgrades.TripleLaunch:
                        this.maxDragCount = 2;
                        break;
                    case (int)UpgradesData.Upgrades.Experience:
                        this.xp = 2;
                        break;
                    case (int)UpgradesData.Upgrades.DragTime:
                        this.dragTimer = 5;
                        break;
                    case (int)UpgradesData.Upgrades.AutoBounce:
                        //TODO make ball to bounce around 5 to 6 balls???? Activable by button????
                        break;
                    case (int)UpgradesData.Upgrades.LaunchGuide:
                        launchGuide = 2000;
                        break;
                }
            }
        }
    }
}
