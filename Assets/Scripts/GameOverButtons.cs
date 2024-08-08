using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverButtons : MonoBehaviour
{
    SceneLoader sceneLoader;

    public void MainMenuButton()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
