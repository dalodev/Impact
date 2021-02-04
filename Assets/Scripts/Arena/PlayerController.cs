using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ball")]
    public Ball ball;
    public GameObject lifeUI;

    public GameController gameController;
    public float dragTimer = 10f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
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
        transform.position = initialPosition;
        this.gameObject.SetActive(true);

    }
}
