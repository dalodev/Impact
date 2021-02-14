using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeTweenManager : MonoBehaviour
{
    public GameObject customizePanel, shopPanel, menuPanel;
    public GameObject nextSkin, previousSkin;
    public GameObject nextTrail, previousTrail;
    public LeanTweenType showType;
    public bool isShown = false;

    private void Start()
    {
        isShown = true;
    }

    public void NextSkin(bool isOn)
    {
        switch (isOn)
        {
            case true:
                LeanTween.scale(nextSkin.GetComponent<RectTransform>(), new Vector3(0.9f, 0.9f, 0.9f) , 0.1f).setEaseLinear().setLoopPingPong(1);
                break;
            case false:
                LeanTween.scale(nextSkin.GetComponent<RectTransform>(), Vector2.zero, 0.1f).setEaseLinear();
                break;
        }
    }

    public void PreviousSkin(bool isOn)
    {
        switch (isOn)
        {
            case true:
                LeanTween.scale(previousSkin.GetComponent<RectTransform>(), new Vector3(0.9f, 0.9f, 0.9f), 0.1f).setEaseLinear().setLoopPingPong(1);
                break;
            case false:
                LeanTween.scale(previousSkin.GetComponent<RectTransform>(), Vector2.zero, 0.1f).setEaseLinear();
                break;
        }
    }

    public void NextTrail(bool isOn)
    {
        switch (isOn)
        {
            case true:
                LeanTween.scale(nextTrail.GetComponent<RectTransform>(), new Vector3(0.9f, 0.9f, 0.9f), 0.1f).setEaseLinear().setLoopPingPong(1);
                break;
            case false:
                LeanTween.scale(nextTrail.GetComponent<RectTransform>(), Vector2.zero, 0.1f).setEaseLinear();
                break;
        }
    }

    public void PreviousTrail(bool isOn)
    {
        switch (isOn)
        {
            case true:
                LeanTween.scale(previousTrail.GetComponent<RectTransform>(), new Vector3(0.9f, 0.9f, 0.9f), 0.1f).setEaseLinear().setLoopPingPong(1);
                break;
            case false:
                LeanTween.scale(previousTrail.GetComponent<RectTransform>(), Vector2.zero, 0.1f).setEaseLinear();
                break;
        }
    }

    public void Shop(bool isOn)
    {
        isShown = !isOn;
        switch (isOn)
        {
            case true:
                shopPanel.SetActive(true);
                LeanTween.scale(shopPanel.GetComponent<RectTransform>(), Vector3.one, 0.4f).setEaseInOutBounce();
                LeanTween.moveY(customizePanel.GetComponent<RectTransform>(), -1650, 0.3f);
                break;
            case false:
                LeanTween.scale(shopPanel.GetComponent<RectTransform>(), Vector3.zero, 0.4f).setEaseInOutBounce();
                LeanTween.moveY(customizePanel.GetComponent<RectTransform>(), 0, 0.3f);
                break;
        }
    }

}
