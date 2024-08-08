using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        public Text textHolder;

        [Header ("Text Options")]
        [SerializeField] public string input;
        [SerializeField] private Color textColor;
        [SerializeField] private Font textFont;

        [Header("Time parameters")]
        [SerializeField] private float delay;
        [SerializeField] private float delayBetweenLines;

        [Header("Sound")]
        [SerializeField] public AudioClip sound;

        [Header("Character Image")]
        [SerializeField] public Sprite characterSprite;
        [SerializeField] public Image imageHolder;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void OnEnable()
        {
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay, sound, delayBetweenLines));
        }

 
    }
}