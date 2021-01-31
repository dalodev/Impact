using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    
    public double coins;
    public Text coinsTxt;

    void Start()
    {
        coinsTxt.text = "$" + coins.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public void Buy()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        ItemInfo item = buttonRef.GetComponent<ItemInfo>();
        if (coins >= item.priceValue && item.enabled == true)
        {
            coins -= item.priceValue;
            item.enabled = false;
            coinsTxt.text = "$" + coins.ToString();
        }

    }

    public void Menu()
    {
        SceneManager.LoadScene(0);

    }
}
