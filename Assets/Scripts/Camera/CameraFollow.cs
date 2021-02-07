using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            targetPosition.y = Mathf.Clamp(targetPosition.y, initialPosition.y, float.MaxValue);
            transform.position = targetPosition;
        }

    }
}
