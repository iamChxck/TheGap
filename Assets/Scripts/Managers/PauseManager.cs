using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    public static PauseManager instance;
    public bool GameIsPaused = false;
    SceneLoader sceneLoader;

    private void Start()
    {
        isPersist = true;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (FindObjectOfType<SettingsManager>() != null)
            sceneLoader.UnloadScene("Settings");
        sceneLoader.UnloadScene("Pause");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadSceneAdd("Pause");
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

}
