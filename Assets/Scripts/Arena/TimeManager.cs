using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public float slowDownFactor = 0.05f;
    public float slowDownLength = 2f;

    public void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void BackToNormal()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

}
