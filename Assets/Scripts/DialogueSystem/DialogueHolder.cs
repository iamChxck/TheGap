using System.Collections;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        DialogueManager manager;
        public bool finished;
     
        private void Awake()
        {
            //StartCoroutine(dialogueSequence());
            finished = true;
        }

        private void Start()
        {

        }

        public void StartDialogueSequence()
        {
            StartCoroutine(DialogueSequence());
        }

        public IEnumerator DialogueSequence()
        {
            finished = false;

            manager = FindObjectOfType<DialogueManager>();
            for (int i = 0; i < manager.lines.Length; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => manager.lines[i].finished);
            }

            finished = true;
            gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                manager.lines[i].gameObject.SetActive(false);
            }
        }
    }
}

