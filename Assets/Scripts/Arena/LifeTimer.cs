using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class LifeTimer : MonoBehaviour
{

    public Image lifeTimer;
    public Ball player;
    private float currentTime;
    private float dragTime = 10f;
    public TimeManager timeManager;

    void Start()
    {
        dragTime = player.dragTimer;
    }

    void Update()
    {

        switch (player.state)
        {
            case Ball.BallState.DRAGGING:
                if(dragTime > 0)
                {
                    dragTime -= Time.unscaledDeltaTime;
                }
                else
                {
                    dragTime = 0f;
                    player.timeDragOut = true;
                    player.Launch();
                    timeManager.BackToNormal();
                }
                currentTime = dragTime;
                lifeTimer.fillAmount = currentTime / player.dragTimer;
                break;
            case Ball.BallState.LAUNCH:
                dragTime = player.dragTimer;
                lifeTimer.fillAmount = player.dragTimer;
                break;
        } 
    }
}
