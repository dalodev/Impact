using UnityEngine;

public class ArenaTweenManager : MonoBehaviour
{

    public GameObject deathPanel;
    public GameObject dragTimer;
    public LeanTweenType deathEasyType;
    public LeanTweenType dragTimerType;

    private void Start()
    {
        LeanTween.scale(dragTimer.GetComponent<RectTransform>(), Vector2.one, 0.5f).setEase(dragTimerType);
        LeanTween.scale(deathPanel.GetComponent<RectTransform>(), Vector2.zero, 0.5f);
    }

    public void ShowDeathPanel(bool isOn)
    {
        switch (isOn)
        {
            case true:
                deathPanel.SetActive(true);
                LeanTween.scale(deathPanel.GetComponent<RectTransform>(), Vector2.one, 0.5f).setEase(deathEasyType);
                LeanTween.scale(dragTimer.GetComponent<RectTransform>(), Vector2.zero, 0.5f).setEase(dragTimerType);
                break;
            case false:
                LeanTween.scale(deathPanel.GetComponent<RectTransform>(), Vector2.zero, 0.5f).setEase(deathEasyType);
                LeanTween.scale(dragTimer.GetComponent<RectTransform>(), Vector2.one, 0.5f).setEase(dragTimerType);
                break;
        }

    }
}
