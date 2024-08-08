using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    SceneLoader sceneLoader;
    [SerializeField] PlayerData playerData;
    SoundManager soundManager;

    private void Start()
    {
        
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager != null)
        {
            if (!soundManager.audSource.isPlaying)
            {

                soundManager.audSource.clip = (AudioClip)Resources.Load("Audio/BGM/MainMenu");
                soundManager.audSource = soundManager.GetComponent<AudioSource>();
                soundManager.audSource.Play();
            }
        }
          
       


    }

    public void PlayButton()
    {
        playerData = Resources.Load<PlayerData>("Prefabs/PlayerData");

        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadScene("Game");

        playerData.isPersist = false;
        playerData.ResetYears();
        playerData.first = true;
    }

    public void TutorialButton()
    {
        playerData = Resources.Load<PlayerData>("Prefabs/PlayerData");
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadScene("Tutorial");


        playerData.isPersist = false;
    }
    public void SettingsButton()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
