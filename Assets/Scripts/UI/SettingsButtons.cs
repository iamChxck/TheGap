using UnityEngine;

public class SettingsButtons : MonoBehaviour
{
    SettingsManager settingsManager;
    SceneLoader sceneLoader;
    public void BackButton()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        Time.timeScale = 1f;
        sceneLoader.LoadScene("Title");
    }

    public void VideoButton()
    {
        settingsManager = FindObjectOfType<SettingsManager>();
        settingsManager.VideoPanel();
    }

    public void AudioButton()
    {
        settingsManager = FindObjectOfType<SettingsManager>();
        settingsManager.AudioPanel();
    }
}
