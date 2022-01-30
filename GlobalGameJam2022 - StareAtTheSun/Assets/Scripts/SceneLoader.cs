using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject mainMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
