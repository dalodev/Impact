using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ball")]
    public Ball ball;

    public GameController gameController;
    public float dragTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        dragTimer = ball.dragTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.canLaunch)
        {
            BallDragging();
        }
    }

    void BallDragging()
    {
        if (Input.GetMouseButtonDown(0)) ball.StartDrag();
        if (Input.GetMouseButton(0)) ball.Dragging();
        if (Input.GetMouseButtonUp(0)) ball.Launch();
    }
}
