﻿using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    public string id;
    public ShopManager.CoinType itemType;
    public string itemName;
    public Sprite sprite;
    public float cost;
    public int reward;
    public Color backgroundColor;
}
