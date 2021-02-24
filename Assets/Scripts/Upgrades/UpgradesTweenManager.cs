using UnityEngine;

public class UpgradesTweenManager : MonoBehaviour
{
    public GameObject upgradesPanel, shopPanel;

    public void Shop(bool isOn)
    {
        switch (isOn)
        {
            case true:
                UiState.instance.SetState(UiState.State.ShopUpgrades);
                shopPanel.SetActive(true);
                LeanTween.scale(shopPanel.GetComponent<RectTransform>(), Vector3.one, 0.4f).setEaseInOutBounce();
                LeanTween.moveY(upgradesPanel.GetComponent<RectTransform>(), -1650, 0.3f);
                break;
            case false:
                UiState.instance.SetState(UiState.State.Upgrades);
                LeanTween.scale(shopPanel.GetComponent<RectTransform>(), Vector3.zero, 0.4f).setEaseInOutBounce();
                LeanTween.moveY(upgradesPanel.GetComponent<RectTransform>(), 0, 0.3f);
                break;
        }

    }
}
