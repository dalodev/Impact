using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrailSkinManager : MonoBehaviour
{
    public GameObject player;
    public List<SkinInfo> skins = new List<SkinInfo>();
    public GameObject lockImage;
    private int currentSkin = 0;
    private int skinIndex = 0;
    private int currentLevel;
    public GameObject nextButton;
    public GameObject previousButton;
    public TextMeshProUGUI skinLevelText;

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
        GetPlayerData();
        SelectSkin(currentSkin);
    }

    private void GetPlayerData()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData();
        if (playerData != null)
        {
            currentLevel = playerData.level;
        }
        else
        {
            currentLevel = 0;
        }
    }

    public void selectCurrentSkin()
    {
        GetPlayerData();
        currentSkin = skins.IndexOf(GetselectedItem());
        skinIndex = currentSkin;
        UpdateArrows();
    }

    private bool FindTrail(SkinInfo item, CustomizeData data)
    {
        bool find = false;
        
        if(item.effect != null)
        {
            find = item.effect.name == data.trail;
        }
        return find;
    }

    public void NextSkin()
    {
        int index = currentSkin += 1;
        if (index < skins.Count-1)
        {
            currentSkin = index;
            previousButton.SetActive(true);
        }
        else
        {
            previousButton.SetActive(true);
            nextButton.SetActive(false);
        }
        SelectSkin(index);
    }

    public void PreviousSkin()
    {
        int index = currentSkin -= 1;
        if (index > 0)
        {
            currentSkin = index;
            nextButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            previousButton.SetActive(false);
        }
        SelectSkin(index);
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

            GameObject newTrail = Instantiate(skin.effect, player.transform.position, Quaternion.identity);
            newTrail.transform.parent = player.transform;
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
        return GetselectedItem().effect.name;
    }

    private void IsSkinEnable(SkinInfo skin)
    {
        if (skin.unlockLevel <= currentLevel)
        {
            lockImage.SetActive(false);
            skinLevelText.gameObject.SetActive(false);
            skin.enabled = true;
        }
        else
        {
            lockImage.gameObject.SetActive(true);
            skinLevelText.gameObject.SetActive(true);
            skinLevelText.text = skin.unlockLevel.ToString();
            skin.enabled = false;
        }
    }

    public void DisableLockImage()
    {
        lockImage.SetActive(false);
    }

    public int GetCurrentSkinCoins()
    {
        return GetselectedItem().coins;
    }

    private void UpdateArrows()
    {
        if (currentSkin == 0)
        {
            nextButton.SetActive(true);
            previousButton.SetActive(false);
        }
        if (currentSkin >= skins.Count-1)
        {
            previousButton.SetActive(true);
            nextButton.SetActive(false);
        }
    }

    public void levelUp()
    {
        currentLevel++;
    }
}
