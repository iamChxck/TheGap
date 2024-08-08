using DialogueSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    public DialogueLine[] lines;
    public DialogueHolder holder;

    public bool inDialogue;
    public GameObject bg;

    PlayerPhysicalForm player;
    public PlayerData playerData;

    public bool playerDialogue,
                narrativeDiaologue;

    public bool firstChore,
                inventoryGuide;
    private void Start()
    {
       

        firstChore = false;
        playerData = Resources.Load<PlayerData>("Prefabs/PlayerData");

        if (playerData.first)
            inventoryGuide = false;
        else
            inventoryGuide = true;

        bg.SetActive(false);
        holder.gameObject.SetActive(false);
        inDialogue = false;

        if (playerDialogue)
        {
            foreach (DialogueLine line in lines)
            {
                line.characterSprite = Resources.Load<Sprite>("Prefabs/DialogueSprite");
            }
        }
        else
        {
            foreach (DialogueLine line in lines)
            {
                line.characterSprite = Resources.Load<Sprite>("Prefabs/BlackBox");
            }
        }

   

        
        if (playerData.first)
            StartCoroutine(InitialDialogue());
        else if (playerData.second)
            StartCoroutine(AppleThirdDialogue());
        else if (playerData.third)
            StartCoroutine(NarrativeThirdDialogue());
        else if (playerData.fourth)
            StartCoroutine(NarrativeFifthDialogue());
        

        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //CHECKS IF THE CURRENT DIALOGUE SEQUENCE IS FINISHED
        if (holder.finished)
            inDialogue = false;
        else
            inDialogue = true;

        if (playerDialogue)
        {
            foreach (DialogueLine line in lines)
            {
                line.characterSprite = Resources.Load<Sprite>("Prefabs/DialogueSprite");
                line.imageHolder.sprite = Resources.Load<Sprite>("Prefabs/DialogueSprite");
            }
        }
        else
        {
            foreach (DialogueLine line in lines)
            {
                line.characterSprite = Resources.Load<Sprite>("Prefabs/BlackBox");
                line.imageHolder.sprite = Resources.Load<Sprite>("Prefabs/BlackBox");
            }
        }


    }


    public IEnumerator InventoryGuide()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = false;
        narrativeDiaologue = true;
        lines[0].input = "You got an Item! Press 'I' to open inventory!";
        holder.StartDialogueSequence();


        yield return new WaitUntil(() => holder.finished);
        inventoryGuide = true;
    }

    public IEnumerator InitialDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        player = FindObjectOfType<PlayerPhysicalForm>();
        player.transform.position = new Vector2(0, -13f);
        lines[0].input = "Apple is a young girl who has lived a sheltered life and is now entering college.";
        lines[1].input = "Her parent's income has afforded the family with a large home with the luxury of living comfortably, and to the young and naive mind, this was her palace and her fortress.";
        lines[2].input = "There has never been any need to bend her back nor crane her neck, thus she has always happily idled away her days.";

        holder.StartDialogueSequence();
        bg.SetActive(true);

        yield return new WaitUntil(() => holder.finished);

        bg.SetActive(false);
        StartCoroutine(AppleFirstDialogue());
    }

    #region APPLE DIALOGUES
    public IEnumerator AppleFirstDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = true;
        narrativeDiaologue = false;
        lines[0].input = "Oh wow, they say the first day in a big school will always shock you, but I never thought the University would be so big!";
        lines[1].input = "I bet it's a hundred times bigger than our house! Hehe, I'm so excited to start my new life as a college gal!";
        holder.StartDialogueSequence();

        yield return new WaitUntil(() => holder.finished);

        StartCoroutine(NarrativeFirstDialogue());

    }

    public IEnumerator AppleSecondDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        player = FindObjectOfType<PlayerPhysicalForm>();
        player.transform.position = new Vector2(-6, -8.5f);
        lines[0].input = "Oh my! What's all this talk about a pandemic? Everyone's so worried about it, but it honestly doesn't seem that bad to me.";
        lines[1].input = "Life carries on as usual, and all we have to do is wear some masks outside.";
        lines[2].input = "They're not even expensive, I don't get what all the fuss is about.";

        playerDialogue = true;
        narrativeDiaologue = false;
        holder.StartDialogueSequence();

        yield return new WaitUntil(() => holder.finished);
    }

    public IEnumerator AppleThirdDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        player = FindObjectOfType<PlayerPhysicalForm>();
        player.transform.position = new Vector2(-5, -1);
        lines[0].input = "I heard mom and dad whispering all worried-like. Is it really that bad? They've always been very capable, \r\nI'm sure they're just overthinking it.";
        lines[1].input = "Money has never been a problem for us, after all, they're the town's best Doctors!";

        playerDialogue = true;
        narrativeDiaologue = false;
        holder.StartDialogueSequence();

        yield return new WaitUntil(() => holder.finished);

        StartCoroutine(NarrativeSecondDialogue());
    }

    public IEnumerator AppleFourthDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        lines[0].input = "Is... is this what it's like to be poor? How do they cope? I can't even focus on school anymore!";
        playerDialogue = true;
        narrativeDiaologue = false;
        holder.StartDialogueSequence();

        yield return new WaitUntil(() => holder.finished);
    }

    public IEnumerator AppleFifthDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        lines[0].input = "Please... no more. Why is life so hard! School is getting harder as the classes become more advanced,";
        lines[1].input = "and the apartment is just falling apart. Where do I even begin? I can't...";
        playerDialogue = true;
        narrativeDiaologue = false;
        holder.StartDialogueSequence();
        Debug.Log("FIFHT DIALOGUE");
        yield return new WaitUntil(() => holder.finished);
    }

    public IEnumerator AppleSixthDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = true;
        narrativeDiaologue = false;
        lines[0].input = "I can't give up now... I'm so close to my dreams. I'll push on, I have to.";
        holder.StartDialogueSequence();


        yield return new WaitUntil(() => holder.finished);

        StartCoroutine(NarrativeSixthDialogue());
    }

    public IEnumerator AppleFirstChoreDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = true;
        narrativeDiaologue = false;
        lines[0].input = "No! This is so unfair! Stupid pandemic! This is making life so unbelievably unbearable!";
        holder.StartDialogueSequence();

        yield return new WaitUntil(() => holder.finished);
       

        //Debug.Log("HI THERE THE FIRST CHORE IS DONE^^");
    }
    #endregion

    #region NARRATIVE DIALOGUES
    public IEnumerator NarrativeFirstDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = false;
        narrativeDiaologue = true;
        lines[0].input = "On her first year in college, she carried on with her life as usual: without a care in the world. The world, in turn, reciprocated.";
        lines[1].input = "Tragedy struck the world as a pandemic spread like wildfire, reaping lives and starving others.";
        holder.StartDialogueSequence();
        bg.SetActive(true);

        yield return new WaitUntil(() => holder.finished);

        bg.SetActive(false);
        StartCoroutine(AppleSecondDialogue());
    }

    public IEnumerator NarrativeSecondDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        player = FindObjectOfType<PlayerPhysicalForm>();
        player.transform.position = new Vector2(-6, 6);
        playerDialogue = false;
        narrativeDiaologue = true;
        lines[0].input = "The life Apple knew burst like a bubble, and ever so suddenly, she is faced with the life she has always hidden away from;";
        lines[1].input = "poverty, physical labor, anxiety, and all manner of struggles came crashing down on her.";
        holder.StartDialogueSequence();
        bg.SetActive(true);

        yield return new WaitUntil(() => holder.finished);

        bg.SetActive(false);
    }

    public IEnumerator NarrativeThirdDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = false;
        narrativeDiaologue = true;
        lines[0].input = "As time went on, her family has had to make difficult decisions to make ends meet.";
        lines[1].input = "Without the financial stability to afford their old life, the house had to be sold, and the family moved into a smaller apartment.";
        lines[2].input = "Sudden unemployment meant that Apple's parents needed to find new work, which unfortunately led them abroad.";
        holder.StartDialogueSequence();
        bg.SetActive(true);

        yield return new WaitUntil(() => holder.finished);

        bg.SetActive(false);
        StartCoroutine(AppleFourthDialogue());
    }

    public IEnumerator NarrativeFourthDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = false;
        narrativeDiaologue = true;
        Debug.Log("DIALOG STARTING");
        lines[0].input = "This new life was a complete mystery to Apple. Gone was the previous unfettered life she once lived. ";
        lines[1].input = "Maintaining a home and taking care of herself became burdens that weighed heavily on her shoulders, making schoolwork monumentally more difficult to a young student like her.";
        holder.StartDialogueSequence();
        bg.SetActive(true);

        yield return new WaitUntil(() => holder.finished);
        Debug.Log("DIALOG FINISHED");
        bg.SetActive(false);
        StartCoroutine(AppleFifthDialogue());
    }

    public IEnumerator NarrativeFifthDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = false;
        narrativeDiaologue = true;
        lines[0].input = "Unable to cope with all her new responsibilities, her world began to crack and dim.";
        lines[1].input = "As the years passed, the struggles only worsened: school became more difficult and her home was falling apart.";
        holder.StartDialogueSequence();
        bg.SetActive(true);

        yield return new WaitUntil(() => holder.finished);

        bg.SetActive(false);
        StartCoroutine(AppleSixthDialogue());
    }

    public IEnumerator NarrativeSixthDialogue()
    {
        ResetDialogue();
        holder.gameObject.SetActive(true);
        playerDialogue = false;
        narrativeDiaologue = true;
        lines[0].input = "And so, she marched on. To see the light at the end of the tunnel, she must brave the darkness and push on.";
        holder.StartDialogueSequence();
        bg.SetActive(true);

        yield return new WaitUntil(() => holder.finished);

        bg.SetActive(false);
    }

    #endregion


    public void FirstChoreDialogue()
    {
        StartCoroutine(AppleFirstChoreDialogue());
    }
    public void SecondChoreDialogue()
    {
        StartCoroutine(NarrativeFourthDialogue());
    }

    private void ResetDialogue()
    {
        foreach (DialogueLine line in lines)
        {
            line.finished = false;
            line.input = string.Empty;
        }
    }
}
