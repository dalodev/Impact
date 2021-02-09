using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinsManager : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public GameObject lockImage;
    public List<SkinInfo> skins = new List<SkinInfo>();
    private int currentSkin = 0;
    private int skinIndex = 0;
    private int currentLevel;
    public TextMeshProUGUI skinCoinsText;
    public GameObject nextButton;
    public GameObject previousButton;

    void Awake()
    {
        CustomizeData data = SaveSystem.LoadCustomize();
        if (data != null)
        {
            SkinInfo skin = skins.Find(item => item.skin.name == data.skin);
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
        SelectSkin(skins[currentSkin]);
    }

    private void SelectSkin(SkinInfo skin)
    {
        if(skin != null)
        {
            IsSkinEnable(skin);
            playerSprite.material = skin.skin;
        }
    }

    public void NextSkin()
    {
        int index = currentSkin += 1;
        if(index < skins.Count)
        {
            previousButton.SetActive(true);
            SkinInfo skin = skins[index];
            if (skin != null)
            {
                IsSkinEnable(skin);
                playerSprite.material = skin.skin;
            }
        }else
        {
            currentSkin = skins.Count-1;
            //hide next button
            nextButton.SetActive(false);
        }
    }

    public void PreviousSkin()
    {
        int index = currentSkin -= 1;
        if (index >= 0)
        {
            nextButton.SetActive(true);
            SkinInfo skin = skins[index];
            if (skin != null)
            {
                IsSkinEnable(skin);
                playerSprite.material = skin.skin;
            }
        }
        else
        {
            currentSkin = 0;
            //hide previous button
            previousButton.SetActive(false);
        }
    }

    public SkinInfo GetselectedItem()
    {
        if (skins[currentSkin].enabled)
        {
            return skins[currentSkin];
        }
        else
        {
            currentSkin = skinIndex;
            playerSprite.material = skins[skinIndex].skin;
            return skins[skinIndex];
        }
        
    }

    public string GetSkinName()
    {
        return GetselectedItem().skin.name;
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
            lockImage.SetActive(true);
            if(skin.coins > 0)
            {

            }
            skin.enabled = false;
        }
    }

    public void DisableLockImage()
    {
        lockImage.SetActive(false);
    }
}
