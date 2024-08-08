using UnityEngine;

public class PlayerPhysicalForm : Entity
{
    [SerializeField]
    protected Inventory inventory;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected Animator anim;

    protected float vertical,
                    horizontal;

    protected bool isWalking,
                   isAstralForm,
                   canMove;

    protected float movementSpeed,
                    activeMoveSpeed;

    protected Vector2 moveDir;
    [SerializeField]
    PlayerData playerData;

    protected DialogueManager dialogueManager;

    [SerializeField]
    protected AudioSource audioSource;

    protected SoundManager soundManager;
    private void Awake()
    {
        canMove = true;
        playerData = Resources.Load<PlayerData>("Prefabs/PlayerData");
        audioSource = GetComponent<AudioSource>();
        inventory = FindObjectOfType<Inventory>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        movementSpeed = 300;
        activeMoveSpeed = movementSpeed;
        if (playerData.isPersist)
            playerData.LoadItems(inventory.items);


    }

    private void OnEnable()
    {
        anim.SetFloat("moveY", 1);
        
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager != null)
        {
            
            soundManager.audSource.clip = (AudioClip)Resources.Load("Audio/BGM/House");
            soundManager.audSource.Play();
        }
        



    }

    protected void FixedUpdate()
    {
        //PLAYER CAN MOVE IF THERE IS NO DIALOGUE HAPPENING
        if (dialogueManager != null)
        {
            if (!dialogueManager.inDialogue)
            {
                if (canMove)
                    Move();
            }
        }
        else if (canMove)
            Move();

        if (rb.velocity.magnitude > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = (AudioClip)Resources.Load("Audio/Player/Walking1");
                audioSource.Play();
            }

        }
        else
            audioSource.Stop();


    }

    protected void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(horizontal, vertical).normalized;

        rb.velocity = new Vector2(horizontal, vertical) * activeMoveSpeed * Time.deltaTime;

        CharacterWalkAnim();
    }


    #region Animations
    void CharacterWalkAnim()
    {

        if (rb.velocity != Vector2.zero)
        {
            isWalking = true;
            anim.SetBool("isWalking", isWalking);
            anim.SetFloat("moveX", rb.velocity.normalized.x);
            anim.SetFloat("moveY", rb.velocity.normalized.y);
        }
        else
        {
            isWalking = false;
            anim.SetBool("isWalking", isWalking);
        }
    }
    #endregion


    #region Collision/Triggers


    #endregion

}
