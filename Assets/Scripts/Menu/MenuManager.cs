using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public MenuTweenManager menuTween;
    public CustomizeTweenManager customizeTween;
    public UpgradesTweenManager upgradesTween;
    public ShopTweenManager shopTween;
    public ArenaTweenManager arenaTween;
    public AudioClip click;

    private void Awake()
    {
        LoadData();
    }

    private void Update()
    {
        OnBackPressed();
    }

    private void OnBackPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBack();
        }
    }

    public void OnBack()
    {
        Debug.Log("OBack " + UiState.instance.GetCurrentState());
        switch (UiState.instance.GetCurrentState())
        {
            case UiState.State.Menu:
                Application.Quit();
                break;
            case UiState.State.Options:
                menuTween.Options(false);
                break;
            case UiState.State.Customize:
                menuTween.Customize(false);
                break;
            case UiState.State.Upgrades:
                menuTween.Upgrades(false);
                break;
            case UiState.State.ShopCustomize:
                customizeTween.Shop(false);
                break;
            case UiState.State.ShopUpgrades:
                upgradesTween.Shop(false);
                break;
        }
        ClickUISound();
    }

    public void ApplyLevel()
    {
        LevelData data = SaveSystem.LoadLevelData();
        if (data != null)
        {
            levelText.text = data.level.ToString();
        }
        else
        {
            levelText.text = 0.ToString();
        }
    }

    public void ClickUISound()
    {
        SfxManager.instance.Play(click);
    }

    public void ShowLeaderBoard()
    {
        GPGSAuthentication.instance.ShowLeaderBoard();
    }
    
    public void LoadData()
    {
        ApplyLevel();
    }

    public void PlayState()
    {
        UiState.instance.SetState(UiState.State.Arena);
    }

    public void GoToPrivacyPolicy()
    {
        Application.OpenURL("https://appsdevidtest.wixsite.com/impact/");
    }
}
