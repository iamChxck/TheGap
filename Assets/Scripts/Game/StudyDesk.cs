using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudyDesk : MonoBehaviour
{
    SceneLoader sceneLoader;
    public GameObject interactBubble;

    private void Start()
    {
        interactBubble.SetActive(true);
    }

    public void EnterAstral()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadScene("AstralFormMain");
        SceneManager.LoadScene("AstralFormMain");
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadScene("AstralFormMain");
        Debug.Log("Enter Astral!");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            interactBubble.SetActive(false);

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            interactBubble.SetActive(true);
    }

}
