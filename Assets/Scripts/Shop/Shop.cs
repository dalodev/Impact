﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Shop : MonoBehaviour
{
    public GameObject menu;
    public TextMeshProUGUI coinsText;
    public MenuManager menuManager;
    [Header("List of items to sold")]
    [SerializeField] private ShopItem[] shopItems;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    private ShopItem itemSelected;

    private void Awake()
    {
        LoadData();
    }

    public void UpdatePlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        string value = string.Format("{0:#,0}", GameObject.FindObjectOfType<ShopManager>().coins.ToString());
        if (data != null)
        {
            value = string.Format("{0:#,0}", data.coins);
        }
        coinsText.text = value;
    }

    private void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            ShopItem item = shopItems[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(item, itemObject));

            itemObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.cost.ToString();
            itemObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "" + item.reward;
            StartCoroutine(LoadPriceRoutine(itemObject, item));
        }
    }

    private void OnButtonClick(ShopItem item, GameObject itemObject)
    {
        itemSelected = item;
        foreach (Transform child in shopContainer)
        {
            child.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
        }
        itemObject.GetComponent<Image>().color = item.backgroundColor;
        menuManager.ClickUISound();
    }

    public ShopItem GetItemSelected()
    {
        return itemSelected;
    }

    public void Buy()
    {
        Debug.Log("buy item: " + GetItemSelected().id);
        IAPManager.instance.BuyCoin(GetItemSelected().id);
    }

    public void LoadData()
    {
        UpdatePlayerData();
    }

    private IEnumerator LoadPriceRoutine(GameObject itemObject, ShopItem item)
    {
        while (!IAPManager.instance.IsInitialized())
            yield return null;

        string loadedPrice = IAPManager.instance.GetProductPriceFromStore(item.id);
        itemObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = loadedPrice;
    }
}
