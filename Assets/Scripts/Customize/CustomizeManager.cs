using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomizeManager : MonoBehaviour
{
    public string skinId, trailId;
    public SkinsManager skinManager;
    public TrailSkinManager trailSkinManager;
    

    void Awake()
    {
        CustomizeData data = SaveSystem.LoadCustomize();
        skinId = data.skin;
        trailId = data.trail;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public void Customize()
    {
        this.skinId = skinManager.getSkinName();
        this.trailId = trailSkinManager.getTrailName();
        SaveSystem.SaveCustomize(this);
        Menu();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
