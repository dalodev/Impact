using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomizeData
{
    public string skin;
    public string trail;

    public CustomizeData(CustomizeManager manager)
    {
        this.skin = manager.skinId;
        this.trail = manager.trailId;
    }
}
