using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailSkinManager : MonoBehaviour
{
    public TrailRenderer trail;
    public GameObject player;
    public List<SkinInfo> skins = new List<SkinInfo>();
    public GameObject lockImage;
    private int currentSkin = 0;
    private int skinIndex = 0;
    private int currentLevel;

    void Awake()
    {
        CustomizeData data = SaveSystem.LoadCustomize();
        if (data != null)
        {
            SkinInfo skin = skins.Find(item => FindTrail(item, data));
            currentSkin = skins.IndexOf(skin);
            skinIndex = skins.IndexOf(skin);
        }
        else
        {
            currentSkin = 0;
            skinIndex = 0;
        }
        
        PlayerData playerData = SaveSystem.LoadPlayerData();
        if(playerData != null)
        {
            currentLevel = playerData.level;
        }
        else
        {
            currentLevel = 0;
        }
        SelectSkin(currentSkin);

    }
    private bool FindTrail(SkinInfo item, CustomizeData data)
    {
        bool find = false;
        if(item.skin != null)
        {
            trail.gameObject.SetActive(true);
            find = item.skin.name == data.trail;
        }
        if(item.effect != null)
        {
            find = item.effect.name == data.trail;
        }
        return find;
    }

    public void NextSkin()
    {
        int index = currentSkin += 1;
        if (index < skins.Count)
        {
            SelectSkin(index);
        }
        else
        {
            currentSkin = skins.Count - 1;
        }
    }

    public void PreviousSkin()
    {
        int index = currentSkin -= 1;
        if (index >= 0)
        {
            SelectSkin(index);
        }
        else
        {
            currentSkin = 0;
        }
    }

    private void SelectSkin(int index)
    {
        SkinInfo skin = skins[index];
        if (skin != null)
        {
            GameObject effect = GameObject.FindGameObjectWithTag("Trail");
            if (effect != null)
            {
                Destroy(effect);
            }
            IsSkinEnable(skin);
               
            Material material = skins[currentSkin].skin;
            if (material != null)
            {
                trail.gameObject.SetActive(true);
                trail.material = skin.skin;
            }
            else
            {
                trail.gameObject.SetActive(false);
                GameObject newTrail = Instantiate(skin.effect, player.transform.position, Quaternion.identity);
                newTrail.transform.parent = player.transform;
            }
            
        }        
    }

    public SkinInfo GetselectedItem()
    {
        GameObject effect = GameObject.FindGameObjectWithTag("Trail");
        
        if (skins[currentSkin].enabled)
        {
            return skins[currentSkin];
        }
        else
        {
            currentSkin = skinIndex;
            if (effect != null)
            {
                Destroy(effect);
            }
            if (skins[skinIndex].skin != null)
            {
                trail.gameObject.SetActive(true);
                trail.material = skins[skinIndex].skin;
            }
            if( skins[skinIndex].effect != null)
            {
                GameObject newTrail = Instantiate(skins[skinIndex].effect, player.transform.position, Quaternion.identity);
                newTrail.transform.parent = player.transform;
            }
            return skins[skinIndex];
        }
        
    }

    public string GetTrailName()
    {
        Material material = GetselectedItem().skin;
        if (material != null)
        {
            return GetselectedItem().skin.name;
        }
        else
        {
            return GetselectedItem().effect.name;
        }

    }

    private void IsSkinEnable(SkinInfo skin)
    {
        if (skin.unlockLevel <= currentLevel)
        {
            lockImage.SetActive(false);
            skin.enabled = true;
        }
        else
        {
            lockImage.gameObject.SetActive(true);
            skin.enabled = false;
        }
    }

    public void DisableLockImage()
    {
        lockImage.SetActive(false);
    }
}
