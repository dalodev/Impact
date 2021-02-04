using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{ 
    public double coins;
    public int level;
    public Text coinsTxt;

    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        this.level = data.level;
        this.coins = data.coins;
        //coinsTxt.text = "$" + coins.ToString();
    }

    void Update()
    {
       
    }

    public void Buy()
    {
        
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
