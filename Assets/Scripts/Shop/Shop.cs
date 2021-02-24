using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject menu;
    [Header("List of items to sold")]
    [SerializeField] private ShopItem[] shopItems;

    [Header("References")]
    [SerializeField] private Transform shopContainer;
    [SerializeField] private GameObject shopItemPrefab;

    private int coins;

    private ShopItem itemSelected;

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

    private void PopulateShop()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            ShopItem item = shopItems[i];
            GameObject itemObject = Instantiate(shopItemPrefab, shopContainer);

            itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(item, itemObject));

            itemObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "" + item.cost + "€";
            itemObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "" + item.reward + " \ncoins";
        }
    }

    private void OnButtonClick(ShopItem item, GameObject itemObject)
    {
        itemSelected = item;
        foreach (Transform child in shopContainer)
        {
            child.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        itemObject.GetComponent<Image>().color = item.backgroundColor;
    }

    public ShopItem GetItemSelected()
    {
        return itemSelected;
    }

    public void Buy()
    {
        //initiate google play services
        //in app service
    }
  
   
}
