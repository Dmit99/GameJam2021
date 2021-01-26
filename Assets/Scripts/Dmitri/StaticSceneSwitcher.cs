using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticSceneSwitcher : MonoBehaviour
{
    public void Go_MainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Go_GameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Go_GameOverScene()
    {
        SceneManager.LoadScene(3);
    }

    public void Go_WinScene()
    {
        SceneManager.LoadScene(2);
    }

    public void Go_CloseGame()
    {
        Application.Quit();
    }
}
