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

    void Start()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        if(data != null)
        {
            this.level = data.level;
            this.coins = data.coins;
        }
        else
        {
            this.level = 0;
            this.coins = 0;
        }
        
        //coinsTxt.text = "$" + coins.ToString();
    }

    public void Buy()
    {
        
    }
}
