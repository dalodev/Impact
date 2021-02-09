using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CustomizeManager : MonoBehaviour
{
    public string skinId, trailId;
    public SkinsManager skinManager;
    public TrailSkinManager trailSkinManager;
    public GameObject menuManager;
    public GameObject customizeManager;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI coinText;
    
    void Awake()
    {
        CustomizeData data = SaveSystem.LoadCustomize();
        if (data != null)
        {
            skinId = data.skin;
            trailId = data.trail;
        }
        else
        {
            skinId = skinManager.GetSkinName();
            trailId = trailSkinManager.GetTrailName();
        }
        PlayerData playerData = SaveSystem.LoadPlayerData();
        if(playerData != null)
        {
            levelText.text =  ""+playerData.level;
        }
        else
        {
            levelText.text = ""+0;
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && this.gameObject.activeInHierarchy)
        {
            menuManager.SetActive(true);
            customizeManager.SetActive(false);
            Customize();
        }
    }

    public void Customize()
    {
        bool navigate = false;
        if (trailSkinManager.GetselectedItem().enabled)
        {
            Debug.Log("trail enabled");
            this.trailId = trailSkinManager.GetTrailName();
            SaveSystem.SaveCustomize(this);
            navigate = true;
        }

        if (skinManager.GetselectedItem().enabled)
        {
            Debug.Log("skin enabled");
            this.skinId = skinManager.GetSkinName();
            SaveSystem.SaveCustomize(this);
            navigate = true;
        }
        if (navigate)
        {
            menuManager.SetActive(true);
            customizeManager.SetActive(false);
            trailSkinManager.DisableLockImage();
            skinManager.DisableLockImage();
        }
        else
        {
            //display error
        }
    }
}
