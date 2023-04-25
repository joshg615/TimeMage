using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentScene : MonoBehaviour
{
    public float delay = 3f; 
    public void StartGame()
    {
        //StartCoroutine("LoadGameScene", delay);
        Invoke("LoadGameScene", delay);
        //SceneManager.LoadScene("Main Game");
    }
    void LoadGameScene()
    {
        //yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowPlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
