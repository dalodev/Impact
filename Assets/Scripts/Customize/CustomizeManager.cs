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
    public AudioClip errorClip;
    public AudioClip confirmationClip;
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
            coins = playerData.coins;
        }
        LevelData levelData = SaveSystem.LoadLevelData();
        if(levelData != null)
        {
            level = levelData.level;
            currentLevel = level;
        }
        coinText.text = string.Format("{0:#,0}", coins);
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

        Debug.Log("skinManager: " + skinManager.GetselectedItem().unlockLevel + " currentLevel: " + currentLevel);
        if (skinManager.GetselectedItem().unlockLevel <= currentLevel)
        {
            Debug.Log("skin enabled");
            this.skinId = skinManager.GetSkinName();
            SaveSystem.SaveCustomize(this);
            navigate = true;
        }
        if (navigate)
        {
            SfxManager.instance.Play(confirmationClip);
            trailSkinManager.DisableLockImage();
            skinManager.DisableLockImage();
        }
        else
        {
            SfxManager.instance.Play(errorClip);
        }
    }
}
