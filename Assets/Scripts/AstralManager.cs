using UnityEngine;

public class AstralManager : MonoBehaviour
{
    SceneLoader sceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (sceneLoader != null)
            sceneLoader.LoadSceneAdd("UI");
        Instantiate(Resources.Load("Prefabs/PlayerAstralForm"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
