using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{

    public int itemID;
    public Text price;
    public double priceValue;
    public GameObject shopManager;

    void Update()
    {
        price.text = "$" + priceValue;
    }
}
