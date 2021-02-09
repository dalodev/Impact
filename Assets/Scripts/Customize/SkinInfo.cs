using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkinInfo
{
    public GameObject effect;
    public Material skin;
    public int itemID;
    public int unlockLevel;
    public bool enabled = false;
    public int coins = 0;
}
