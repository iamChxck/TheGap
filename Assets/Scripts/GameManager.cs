using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        sceneLoader.LoadSceneAdd("Title");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
