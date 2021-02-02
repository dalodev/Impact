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
    public string itemName;
    public int unlockLevel;
    public bool enabled = false;
}
