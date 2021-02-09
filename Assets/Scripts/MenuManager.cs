using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        PlayerData playerData = SaveSystem.LoadPlayerData();
        if(playerData != null)
        {
            levelText.text = "Level "+ playerData.level;
        }
        else
        {
            levelText.text = "Level " + 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
