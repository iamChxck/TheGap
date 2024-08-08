using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] public Slider volumeSlider;
    // Start is called before the first frame update

    [SerializeField] public AudioSource audSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(FindObjectOfType<SoundManager>().gameObject);
            DontDestroyOnLoad(gameObject);
        }

    }



    void Start()
    {

        PlayerPrefs.DeleteKey("MasterVolume");
        audSource = GetComponent<AudioSource>();
        //DontDestroyOnLoad(this.gameObject);
        if (!PlayerPrefs.HasKey("mastervolume"))
        {
            PlayerPrefs.SetFloat("mastervolume", 1);
        }
        else
        {
            Load();
        }


    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (FindObjectOfType<PlayerPhysicalForm>() != null)
        {
            Debug.Log("FOUND PLAYER");
            audSource.Stop();
            audSource.clip = (AudioClip)Resources.Load("Audio/BGM/House");
            if (!audSource.isPlaying)
                audSource.Play();
        }

        if (FindObjectOfType<PlayerAstralForm>() != null)
        {
            audSource.Stop();
            audSource.clip = (AudioClip)Resources.Load("Audio/BGM/AstralForm");
            if (!audSource.isPlaying)
                audSource.Play();
        }
        */
    }

    public void PlaySound(AudioClip clip)
    {

    }

    public void PlayMusic(AudioClip clip)
    {

    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        if (volumeSlider != null)
            volumeSlider.value = PlayerPrefs.GetFloat("mastervolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("mastervolume", volumeSlider.value);
    }
}
