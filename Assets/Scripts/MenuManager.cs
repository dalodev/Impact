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
            levelText.text = playerData.level.ToString();
        }
        else
        {
            levelText.text = 0.ToString();
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
