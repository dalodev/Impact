using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ball")]
    public Ball ball;
    public GameObject lifeUI;

    public GameController gameController;
    public float dragTimer = 10f;

    void Start()
    {
        lifeUI.SetActive(true);
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

    public void RestartPosition()
    {
        gameController.RemoveSpawnerEnemies();
        gameObject.SetActive(true);
        gameController.PlayAgain();
        gameObject.transform.position = Vector3.zero;
        ball.ResetLaunch(true);
        ball.EnableAutobounce();
    }
}
