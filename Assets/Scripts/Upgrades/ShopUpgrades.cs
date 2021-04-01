using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopUpgrades : MonoBehaviour
{
    [Header("List of items to sold")]
    [SerializeField] public UpgradeItem[] items;
    [SerializeField] public int levelUpMultipler = 1;

    [Header("References")]
    [SerializeField] private Transform upgradesContainer;
    [SerializeField] private GameObject upgradesItemPrefab;

    public MenuManager menuManager;
    public GameController gameController;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI levelText;
    private UpgradeItem itemSelected;
    private GameObject gameObjectSelected;
    public List<int> myItems = new List<int>();
    public AudioClip confirmationClip;
    public AudioClip errorClip;
    public bool test;

    private int coins;
    private int level;

    private void Awake()
    {
        LoadData();
    }

    public void UpdatePlayerData()
    {
        LevelData levelData = SaveSystem.LoadLevelData();
        if(levelData!= null)
        {
            level = levelData.level;
        }
        PlayerData playerData = SaveSystem.LoadPlayerData();
        if (playerData != null)
        {
            coins = playerData.coins;
        }
        if (test)
        {
            coins = 1000000;
        }
        coinsText.text = string.Format("{0:#,0}", coins);
        levelText.text = level.ToString();
    }

    private void LoadDataUpgradesData()
    {
        UpgradesData upgrades = SaveSystem.LoadUpgrades();
        levelUpMultipler = 1;
        myItems.Clear();
        foreach(Transform child in upgradesContainer)
        {
            GameObject.Destroy(child.transform);
        }
        if (upgrades != null)
        {
            levelUpMultipler = upgrades.levelUpMultipler;

            foreach (int i in upgrades.items)
            {
                this.myItems.Add(i);
            }
        }
        PopulateShop();
    }

    private void PopulateShop()
    {
        for (int i = 0; i < items.Length; i++)
        {
            UpgradeItem item = items[i];
            GameObject itemObject = Instantiate(upgradesItemPrefab, upgradesContainer);

            itemObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(item, itemObject));

            string costFormat = string.Format("{0:#,0}", item.cost);
            itemObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = costFormat;
            itemObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "" + item.itemName;
            if (item.sprite)
            {
                itemObject.transform.GetChild(2).GetComponent<Image>().sprite = item.sprite;
            }
           
            foreach (int id in myItems)
            {
                if(id == item.id)
                {
                    itemObject.transform.GetComponent<Image>().color = item.buyColor;
                    //itemObject.transform.GetChild(5).GetComponent<Image>().gameObject.SetActive(true);
                    UpdateLevelUp(itemObject, item);
                    UpdateLove(itemObject, item);
                }
            }
        }     
    }

    private void UpdateLevelUp(GameObject itemObject, UpgradeItem item)
    {
        if(item.id == (int)UpgradesData.Upgrades.LevelUp)
        {
            if (myItems.Contains((int)item.id))
            {
                if(GetItemSelected() != null && GetItemSelected().id == item.id)
                {
                    itemObject.transform.GetComponent<Image>().color = item.backgroundColor;
                }
                else
                {
                    itemObject.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
                    //itemObject.transform.GetChild(5).GetComponent<Image>().gameObject.SetActive(false);
                }
                string value = string.Format("{0:#,0}", item.cost * levelUpMultipler);
                itemObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = value;
            }
        }
    }

    private void UpdateLove(GameObject itemObject, UpgradeItem item)
    {
        if(item.id == (int)UpgradesData.Upgrades.Love)
        {
            if (myItems.Contains((int)item.id))
            {
                if(GetItemSelected() != null && GetItemSelected().id == item.id)
                {
                    itemObject.transform.GetComponent<Image>().color = item.backgroundColor;
                }
                else
                {
                    itemObject.transform.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
                }
                string value = string.Format("{0:#,0}", item.cost * levelUpMultipler);
                itemObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = value;
            }
        }
    }

    private void OnButtonClick(UpgradeItem item, GameObject itemObject)
    {
        itemSelected = item;
        gameObjectSelected = itemObject;
        foreach (Transform child in upgradesContainer)
        {
            if(child.gameObject.GetComponent<Image>().color != item.buyColor)
            {
                child.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
            }
        }
        if (itemObject.GetComponent<Image>().color != item.buyColor)
        {
            itemObject.GetComponent<Image>().color = item.backgroundColor;
        }
        menuManager.ClickUISound();
    }

    public UpgradeItem GetItemSelected()
    {
        return itemSelected;
    }

    public void Buy()
    {
        bool canBuy = true;
        if (myItems.Count > 0)
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                if (GetItemSelected() != null && myItems[i] == GetItemSelected().id)
                {
                    if (myItems[i] == (int)UpgradesData.Upgrades.LevelUp || myItems[i] == (int)UpgradesData.Upgrades.Love)
                    {
                        canBuy = true;
                    }
                    else
                    {
                        canBuy = false;
                    }
                }
                
            }
        }
        if(GetItemSelected() != null)
        {
            if (GetItemSelected().cost <= coins && canBuy)
            {
                if(GetItemSelected().id == (int)UpgradesData.Upgrades.LevelUp)
                {
                    Debug.Log("levelupMultiplier " + (int)GetItemSelected().cost * levelUpMultipler);
                    this.coins -= (int)GetItemSelected().cost * levelUpMultipler;
                }
                else
                {
                    this.coins -= (int)GetItemSelected().cost;
                }
                coinsText.text = string.Format("{0:#,0}", coins);
                Debug.Log("Buy it");
                if (!myItems.Contains((int)GetItemSelected().id))
                {
                    this.myItems.Add((int)GetItemSelected().id);
                }
                gameObjectSelected.GetComponent<Image>().color = GetItemSelected().buyColor;
                //gameObjectSelected.transform.GetChild(5).GetComponent<Image>().gameObject.SetActive(true);
                if (GetItemSelected().id == (int)UpgradesData.Upgrades.LevelUp)
                {
                    levelUpMultipler += 1;
                    UpdateLevelUp(gameObjectSelected, GetItemSelected());
                    GameObject.FindObjectOfType<GameController>().LevelUp();
                    menuManager.ApplyLevel();
                    level += 1;
                    levelText.text = "" + level;
                }
                if (GetItemSelected().id == (int)UpgradesData.Upgrades.Love)
                {
                    gameObjectSelected.GetComponent<Image>().color = new Color32(255, 255, 255, 50);
                    //gameObjectSelected.transform.GetChild(5).GetComponent<Image>().gameObject.SetActive(false);
                }
                SaveSystem.SaveUpgrades(this);
                gameController.ApplyUpgrades(coins);
              
                SfxManager.instance.Play(confirmationClip);
            }
            else
            {
                Debug.Log("You can't buy this upgrade :(");
                SfxManager.instance.Play(errorClip);
            }
        }
    }

    public void LoadData()
    {
        coins = 0;
        level = 0;
        UpdatePlayerData();
        LoadDataUpgradesData();
    }
}
