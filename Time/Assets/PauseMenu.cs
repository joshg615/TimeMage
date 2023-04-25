using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject howToPlayMenu;
    public GameObject playerHealth; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            playerHealth.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void Resume()
    {
        playerHealth.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void HowToPlay()
    {
        pauseMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }

    public void pauseFromHowTo()
    {
        howToPlayMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void pauseFromButton()
    {
        playerHealth.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
