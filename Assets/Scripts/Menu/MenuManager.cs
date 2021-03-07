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

    private void Awake()
    {
        ApplyLevel();
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
        switch (UiState.instance.GetCurrentState())
        {
            case UiState.State.Menu:
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
    }

    public void ApplyLevel()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData();
        if (playerData != null)
        {
            levelText.text = playerData.level.ToString();
        }
        else
        {
            levelText.text = 0.ToString();
        }
    }
}
