using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Shop()
    {
        SceneManager.LoadScene(2);
    }


}
