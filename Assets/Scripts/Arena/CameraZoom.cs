using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target;
    public Ball currentPlayer;

    public float maxOrthographicSize = 40f;
    public float minOrthographicSize = 25f;
    public float smooth = 5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPlayer.state)
        {
            case Ball.BallState.LAUNCH:
                Camera.main.orthographicSize = Mathf.Lerp(minOrthographicSize, maxOrthographicSize, Time.deltaTime * smooth);
                break;
            case Ball.BallState.DRAGGING:
                Camera.main.orthographicSize = Mathf.Lerp(maxOrthographicSize, minOrthographicSize, Time.deltaTime * smooth);
                break;
        }
    }
}
