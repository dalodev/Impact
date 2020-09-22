using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject deathText;
    private bool canRestart = false;

    private void Update()
    {
        if (canRestart && Input.GetMouseButton(0))
        {
            Restart();
        }
    }

    public void PlayerDead()
    {
        deathText.SetActive(true);
        canRestart = true;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
