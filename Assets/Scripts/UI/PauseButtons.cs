using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseButtons : MonoBehaviour
{

    PauseManager pauseManager;
    SceneLoader sceneLoader;
    public void Resume()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        pauseManager = FindObjectOfType<PauseManager>();

        pauseManager.Resume();
    }

    public void MainMenu()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();

        Time.timeScale = 1f;
        pauseManager.GameIsPaused = false;
        sceneLoader.LoadScene("Title");
   
    }

    public void Settings()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        pauseManager = FindObjectOfType<PauseManager>();

        sceneLoader.LoadSceneAdd("Settings");
    }

    public void Quit()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        Application.Quit();
    }
}
