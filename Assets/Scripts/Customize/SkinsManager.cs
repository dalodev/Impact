using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinsManager : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem playerParticleSys;
    public GameObject lockImage;
    public List<SkinInfo> skins = new List<SkinInfo>();
    private int currentSkin = 0;
    private int skinIndex = 0;
    private int currentLevel;
    public TextMeshProUGUI skinCoinsText;
    public TextMeshProUGUI skinLevelText;
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
        GetPlayerData();
        UpdateArrows();
        SelectSkin(skins[currentSkin]);
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

    private void SelectSkin(SkinInfo skin)
    {
        if(skin != null)
        {
            IsSkinEnable(skin);
            playerParticleSys.GetComponent<ParticleSystemRenderer>().material = skin.skin;
        }
    }

    public void NextSkin()
    {

        int index = currentSkin += 1;
        if(index < skins.Count-1)
        {
            currentSkin = index;
            previousButton.SetActive(true);
        }
        else
        {
            previousButton.SetActive(true);
            nextButton.SetActive(false);
        }
        SkinInfo skin = skins[index];
        if (skin != null)
        {
            IsSkinEnable(skin);
            DisplaySkinMaterial(skin);
            GameObject effect = GameObject.FindGameObjectWithTag("Skin");
            if (effect != null)
            {
                Destroy(effect);
            }
            DisplaySkinEffect(skin);
        }
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
        SkinInfo skin = skins[index];
        if (skin != null)
        {
            IsSkinEnable(skin);
            DisplaySkinMaterial(skin);
            GameObject effect = GameObject.FindGameObjectWithTag("Skin");
            if (effect != null)
            {
                Destroy(effect);
            }
            DisplaySkinEffect(skin);
        }
    }

    private void DisplaySkinMaterial(SkinInfo skin)
    {
        if (skin.skin != null)
        {
            playerParticleSys.gameObject.SetActive(true);
            playerParticleSys.GetComponent<ParticleSystemRenderer>().material = skin.skin;
        }
    }

    private void DisplaySkinEffect(SkinInfo skin)
    {
        if (skin.effect != null)
        {
            playerParticleSys.gameObject.SetActive(false);
            GameObject newSkin = Instantiate(skin.effect, player.transform.position, Quaternion.identity);
            newSkin.transform.parent = player.transform;
        }
    }

    public SkinInfo GetselectedItem()
    {
        if (skins[currentSkin].unlockLevel <= currentLevel)
        {
            return skins[currentSkin];
        }
        else
        {
            GameObject effect = GameObject.FindGameObjectWithTag("Skin");

            currentSkin = skinIndex;
            IsSkinEnable(skins[skinIndex]);
            DisplaySkinMaterial(skins[skinIndex]);
            if (effect != null)
            {
                Destroy(effect);
            }
            DisplaySkinEffect(skins[skinIndex]);

            return skins[skinIndex];
        }

    }

    public string GetSkinName()
    {
        if (GetselectedItem().effect != null)
        {
            return GetselectedItem().effect.name;
        }
        if (GetselectedItem().skin != null)
        {
            return GetselectedItem().skin.name;
        }
        return null;
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
            lockImage.SetActive(true);
            skinLevelText.gameObject.SetActive(true);
            skinLevelText.text = "" + skin.unlockLevel;
            skin.enabled = false;
        }
    }

    public void DisableLockImage()
    {
        lockImage.SetActive(false);
    }

    private void UpdateArrows()
    {
        if(currentSkin == 0)
        {
            nextButton.SetActive(true);
            previousButton.SetActive(false);
        }
        if(currentSkin == skins.Count - 1)
        {
            nextButton.SetActive(false);
            previousButton.SetActive(true);
        }
        if(currentSkin > 0 && currentSkin < skins.Count - 1)
        {
            previousButton.SetActive(true);
            nextButton.SetActive(true);
        }
    }
    public void levelUp()
    {
        currentLevel++;
    }
}
