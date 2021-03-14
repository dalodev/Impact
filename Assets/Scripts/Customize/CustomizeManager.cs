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
    private int currentLevel = 0;
    
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
        UpdatePlayerData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UiState.instance.GetCurrentState() == UiState.State.Customize)
        {
            Customize();
        }
    }

    public void UpdatePlayerData()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData();
        int coins = 0;
        int level = 0;
        if (playerData != null)
        {
            level = playerData.level;
            coins = playerData.coins;
        }
        coinText.text = coins.ToString();
        levelText.text = level.ToString();
    }

    public void Customize()
    {
        bool navigate = false;
        if (trailSkinManager.GetselectedItem().unlockLevel <= currentLevel)
        {
            Debug.Log("trail enabled");
            this.trailId = trailSkinManager.GetTrailName();
            SaveSystem.SaveCustomize(this);
            navigate = true;
        }

        if (skinManager.GetselectedItem().unlockLevel <= currentLevel)
        {
            Debug.Log("skin enabled");
            this.skinId = skinManager.GetSkinName();
            SaveSystem.SaveCustomize(this);
            navigate = true;
        }
        if (navigate)
        {
            //menuManager.SetActive(true);
            //customizeManager.SetActive(false);
            trailSkinManager.DisableLockImage();
            skinManager.DisableLockImage();
        }
        else
        {
            //display error
        }
    }

    public void DebugLevelUp()
    {
        trailSkinManager.levelUp();
        skinManager.levelUp();
        levelText.text = "" + currentLevel++; 
    }
}
