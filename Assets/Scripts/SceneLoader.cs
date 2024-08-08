using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    List<string> scenes = new List<string>();



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(FindObjectOfType<SceneLoader>().gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

 

    public void LoadScene(string sceneName)
    {
   
      
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void LoadSceneAdd(string sceneName)
    {
       
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    
}
