using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSkinManager : MonoBehaviour
{
    public TrailRenderer trail;
    public List<SkinInfo> skins = new List<SkinInfo>();
    private int currentSkin = 0;

    void Awake()
    {
        CustomizeData data = SaveSystem.LoadCustomize();
        SkinInfo skin = skins.Find(item => item.itemName == data.trail);
        currentSkin = skins.IndexOf(skin);
        selectSkin(skin);

    }

    private void selectSkin(SkinInfo skin)
    {
        if (skin != null)
        {
            trail.material = skin.skin;
        }
    }

    public void nextSkin()
    {
        int index = currentSkin += 1;
        if (index < skins.Count)
        {
            SkinInfo skin = skins[index];
            if (skin != null)
            {
                trail.material = skin.skin;

            }
        }
        else
        {
            currentSkin = skins.Count - 1;
        }
    }

    public void previousSkin()
    {
        int index = currentSkin -= 1;
        if (index >= 0)
        {
            SkinInfo skin = skins[index];
            if (skin != null)
            {
                trail.material = skin.skin;
            }
        }
        else
        {
            currentSkin = 0;
        }
    }

    public SkinInfo getselectedItem()
    {
        return skins[currentSkin];
    }


    public string getTrailName()
    {
        return skins[currentSkin].skin.name;
    }
}
