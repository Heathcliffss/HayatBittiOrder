using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("Settings Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
