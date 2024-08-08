using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField]
    ProgressBar progressBar;
    [SerializeField]
    GameObject thoughtBubble;
    private bool canTeleport;
    private bool canAstral;
    // Start is called before the first frame update
    void Start()
    {
        canTeleport = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chore")
        {
            Debug.Log(collision.gameObject.name);
            thoughtBubble.SetActive(true);
        }

        if (collision.gameObject.tag == "BathEnter")
        {
            gameObject.transform.position = new Vector2(-4.5f, GameObject.FindGameObjectWithTag("BathExit").gameObject.transform.position.y - 1);
        }

        if (collision.gameObject.tag == "BathExit")
        {
            gameObject.transform.position = new Vector2(-4.5f, GameObject.FindGameObjectWithTag("BathEnter").gameObject.transform.position.y + 1);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chore")
        {

            thoughtBubble.SetActive(true);
            Chore chore = collision.gameObject.GetComponent<Chore>();
            if (Input.GetKey(KeyCode.E))
            {
                progressBar.gameObject.SetActive(true);
                thoughtBubble.SetActive(false);
                progressBar.IncrementProgress();
                if (progressBar.slider.value == progressBar.slider.maxValue)
                {
                    progressBar.gameObject.SetActive(false);
                    progressBar.Reset();
                    chore.ChoreCompletion();
                }
            }
        }

        if (collision.gameObject.tag == "StudyDesk")
        {
            thoughtBubble.SetActive(true);
            Debug.Log("StudyDesk in range!");
            if (Input.GetKey(KeyCode.E))
            {
                thoughtBubble.SetActive(false);
                StudyDesk studyDesk = collision.gameObject.GetComponent<StudyDesk>();

                studyDesk.EnterAstral();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (thoughtBubble != null)
            thoughtBubble.SetActive(false);
        progressBar.gameObject.SetActive(false);
        progressBar.Reset();
    }
}
