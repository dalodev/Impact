using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTweenManager : MonoBehaviour
{
    public CustomizeTweenManager customizeTween;
    public GameObject menuPanel, customizePanel;
    public GameObject level;
    public GameObject playBtn, customizeBtn, upgradesBtn, privacyBtn, leaderboardBtn, settingsBtn;
    public LeanTweenType buttonsType;
    public LeanTweenType topButtonsType;
    public bool isShown = true;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveY(level.GetComponent<RectTransform>(), 200, 0.3f);

        LeanTween.moveY(privacyBtn.GetComponent<RectTransform>(), -294, 0.4f).setEase(topButtonsType);
        LeanTween.moveY(leaderboardBtn.GetComponent<RectTransform>(), -350, 0.4f).setEase(topButtonsType);
        LeanTween.moveY(settingsBtn.GetComponent<RectTransform>(), -300, 0.4f).setEase(topButtonsType);

        LeanTween.moveX(playBtn.GetComponent<RectTransform>(), 0, 0.5f).setEase(buttonsType);
        LeanTween.moveX(customizeBtn.GetComponent<RectTransform>(), 0, 0.5f).setEase(buttonsType);
        LeanTween.moveX(upgradesBtn.GetComponent<RectTransform>(), 0, 0.5f).setEase(buttonsType);
    }

    public void Customize(bool isOn)
    {
        isShown = !isOn;
        switch (isOn)
        {
            case true:
                LeanTween.moveX(menuPanel.GetComponent<RectTransform>(), -800, 0.4f).setEaseInSine();
                LeanTween.moveX(customizePanel.GetComponent<RectTransform>(), 0, 0.4f).setEaseInSine();
                break;
            case false:
                LeanTween.moveX(menuPanel.GetComponent<RectTransform>(), 0, 0.4f).setEaseInSine();
                LeanTween.moveX(customizePanel.GetComponent<RectTransform>(), 800, 0.4f).setEaseInSine();

                break;
        }

    }
   
}
