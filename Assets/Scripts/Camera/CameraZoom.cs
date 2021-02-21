using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target;
    public Ball currentPlayer;

    public float maxOrthographicSize = 40f;
    public float minOrthographicSize = 25f;
    public float smooth = 15f;
    private float zoomFactor = 10f;
    private float targetZoom;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPlayer.state)
        {
            case Ball.BallState.LAUNCH:
                targetZoom -= 0.1f * zoomFactor;
                targetZoom = Mathf.Clamp(targetZoom, minOrthographicSize, maxOrthographicSize);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.unscaledDeltaTime * smooth);
                break;
            case Ball.BallState.DRAGGING:
                targetZoom -= -0.1f * zoomFactor;
                targetZoom = Mathf.Clamp(targetZoom, minOrthographicSize, maxOrthographicSize);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.unscaledDeltaTime * smooth);
                break;
        }
    }
}
