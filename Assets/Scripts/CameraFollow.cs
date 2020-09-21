using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = targetPosition;
    }
}
