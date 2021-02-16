using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 0.6f;

    public TimeManager timeManager;
    public BallState state = BallState.NONE;

    [Header("Stats")]
    public float dragTimer = 10f;
    public float xp;

    [Header("Game")]
    public GameController gameCotroller;

    public bool timeDragOut = false;

    public Camera myCamera;
    Vector3 endPoint;

    public bool canLaunch = true;

    [Header("Customize")]
    public TrailRenderer trail; 

    void Awake()
    {
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
        canLaunch = false;
        state = BallState.LAUNCH;
        if (!timeDragOut)
        {
            rb.velocity = Vector2.zero;
            timeManager.BackToNormal();
            endPoint = myCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 launchForce = GetLaunchDirection(endPoint);
            Vector2 clampForce = Vector2.ClampMagnitude(launchForce, maxDrag);
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

        int iterations = 650 / (int) vel.magnitude;
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
        gameCotroller.PlayerDead();
        gameObject.SetActive(false);
    }

   public enum BallState
    {
        NONE,
        DRAGGING,
        LAUNCH
    }

}
