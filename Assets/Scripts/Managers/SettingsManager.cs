using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    public GameObject videoPanel, audioPanel;
    public Slider masterVolumeSlider;
    [SerializeField]
    SoundManager soundManager;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        masterVolumeSlider.value = PlayerPrefs.GetFloat("mastervolume");
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        GetResolutions();
    }

    private void GetResolutions()
    {
        //Get a list of all viable Resolutions according to the user's hardware
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void ClearUI()
    {
        videoPanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    public void VideoPanel()
    {
        ClearUI();
        if (videoPanel.activeInHierarchy == false)
        {
            videoPanel.SetActive(true);
        }
        else
            videoPanel.SetActive(false);

    }

    public void AudioPanel()
    {
        ClearUI();
        if (audioPanel.activeInHierarchy == false)
        {
            audioPanel.SetActive(true);
        }
        else
            audioPanel.SetActive(false);

    }

    #region VideoPanel
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #endregion

    #region AudioPanel

    public void MasterVolumeAdjust()
    {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.volumeSlider = masterVolumeSlider;
        soundManager.ChangeVolume();
    }

    #endregion
}
