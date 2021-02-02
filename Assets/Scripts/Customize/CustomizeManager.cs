using UnityEngine;
using UnityEngine.SceneManagement;


public class CustomizeManager : MonoBehaviour
{
    public string skinId, trailId;
    public SkinsManager skinManager;
    public TrailSkinManager trailSkinManager;
    public GameObject menuManager;
    public GameObject customizeManager;
    
    void Awake()
    {
        CustomizeData data = SaveSystem.LoadCustomize();
        skinId = data.skin;
        trailId = data.trail;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuManager.SetActive(true);
            customizeManager.SetActive(false);
        }
           
    }

    public void Customize()
    {
        this.skinId = skinManager.getSkinName();
        this.trailId = trailSkinManager.getTrailName();
        SaveSystem.SaveCustomize(this);
    }
}
