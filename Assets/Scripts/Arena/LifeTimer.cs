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

    // Start is called before the first frame update
    void Start()
    {
        dragTime = player.dragTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player.state == Ball.BallState.DRAGGING)
        {
            if(dragTime > 0)
            {
                dragTime -= Time.unscaledDeltaTime;
            }
            else
            {
                player.timeDragOut = true;
                player.Launch();
                dragTime = 0f;
                timeManager.BackToNormal();
            }
            currentTime = dragTime;
            lifeTimer.fillAmount = currentTime / player.dragTimer;
        }
        if(player.state == Ball.BallState.LAUNCH)
        {
            dragTime = player.dragTimer;
            lifeTimer.fillAmount = player.dragTimer;
        }
    }
}
