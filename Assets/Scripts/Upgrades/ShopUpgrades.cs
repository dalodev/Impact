using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUpgrades : MonoBehaviour
{
    [Header("List of items to sold")]
    [SerializeField] private UpgradeItem[] items;

    [Header("References")]
    [SerializeField] private Transform upgradesContainer;
    [SerializeField] private GameObject upgradesItemPrefab;

    public MenuTweenManager menuTweenManager;
    private UpgradeItem itemSelected;

    private int coins;

    private void Awake()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData();
        if (playerData != null)
        {
            coins = playerData.coins;
        }
    }

    private void Start()
    {
        PopulateShop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && this.gameObject.activeInHierarchy)
        {
            //GO TO MENU
        }
    }

    private void PopulateShop()
    {
        for (int i = 0; i < items.Length; i++)
        {
            UpgradeItem item = items[i];
            GameObject itemObject = Instantiate(upgradesItemPrefab, upgradesContainer);

            itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(item, itemObject));

            itemObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + item.cost + "€";
        }
    }

    private void OnButtonClick(UpgradeItem item, GameObject itemObject)
    {
        itemSelected = item;
        foreach (Transform child in upgradesContainer)
        {
            child.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        itemObject.GetComponent<Image>().color = item.backgroundColor;
    }

    public UpgradeItem GetItemSelected()
    {
        return itemSelected;
    }
    public void Buy()
    {
       if(GetItemSelected().cost <= coins)
        {
            //buy item
            menuTweenManager.Upgrades(false);
        }
        else
        {
            //show not enogh coins
            //redirect to shop
        }
    }
}
