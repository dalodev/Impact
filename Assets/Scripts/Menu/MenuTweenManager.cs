using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTweenManager : MonoBehaviour
{
    public CustomizeTweenManager customizeTween;
    public GameObject menuPanel, customizePanel, optionsPanel, upgradesPanel;
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

    public void Options(bool isOn)
    {
        switch (isOn)
        {
            case true:
                optionsPanel.SetActive(true);
                Settings settings = optionsPanel.GetComponent<Settings>();
                settings.isShown = true;
                LeanTween.moveY(playBtn.GetComponent<RectTransform>(), -900, 0.3f).setEaseInOutBack();
                LeanTween.moveY(customizeBtn.GetComponent<RectTransform>(), -1200, 0.3f).setEaseInOutBack();
                LeanTween.moveY(upgradesBtn.GetComponent<RectTransform>(), -1400, 0.3f).setEaseInOutBack();
                LeanTween.moveY(level.GetComponent<RectTransform>(), 900, 0.3f).setEaseInOutBack();
                LeanTween.moveY(privacyBtn.GetComponent<RectTransform>(), 24, 0.3f).setEaseInOutBack();
                LeanTween.moveY(leaderboardBtn.GetComponent<RectTransform>(), 108, 0.3f).setEaseInOutBack();
                LeanTween.moveY(settingsBtn.GetComponent<RectTransform>(), 18, 0.3f).setEaseInOutBack();
                LeanTween.scale(optionsPanel.GetComponent<RectTransform>(), Vector2.one, 0.6f).setEaseOutBounce();
                break;
            case false:
                LeanTween.moveY(playBtn.GetComponent<RectTransform>(), -150, 0.3f).setEaseInOutBack();
                LeanTween.moveY(customizeBtn.GetComponent<RectTransform>(), -300, 0.3f).setEaseInOutBack();
                LeanTween.moveY(upgradesBtn.GetComponent<RectTransform>(), -450, 0.3f).setEaseInOutBack();
                LeanTween.moveY(level.GetComponent<RectTransform>(), 200, 0.3f).setEaseInOutBack();
                LeanTween.moveY(privacyBtn.GetComponent<RectTransform>(), -294, 0.3f).setEaseInOutBack();
                LeanTween.moveY(leaderboardBtn.GetComponent<RectTransform>(), -350, 0.3f).setEaseInOutBack();
                LeanTween.moveY(settingsBtn.GetComponent<RectTransform>(), -300, 0.3f).setEaseInOutBack();
                LeanTween.scale(optionsPanel.GetComponent<RectTransform>(), Vector2.zero, 0.3f).setEaseInOutBack();
                break;
        }
    }

    public void Upgrades(bool isOn)
    {
        switch (isOn)
        {
            case true:
                upgradesPanel.SetActive(true);
                LeanTween.moveY(upgradesPanel.GetComponent<RectTransform>(), 0, 0.3f).setEaseInOutBack();
                LeanTween.moveY(menuPanel.GetComponent<RectTransform>(), -1700, 0.3f).setEaseInOutBack();
                break;
            case false:
                LeanTween.moveY(upgradesPanel.GetComponent<RectTransform>(), 1700, 0.3f).setEaseInOutBack();
                LeanTween.moveY(menuPanel.GetComponent<RectTransform>(), 0, 0.3f).setEaseInOutBack();
                break;
        }
    }
   
}
