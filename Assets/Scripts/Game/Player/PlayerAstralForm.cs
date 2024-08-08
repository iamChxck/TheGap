using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerAstralForm : PlayerPhysicalForm
{

    SceneLoader sceneLoader;
    bool isDead, isAttacking, isShooting;
    public Dictionary<string, GameObject> hitBoxes = new Dictionary<string, GameObject>();

    PlayerData playerData;
    PlayerHealth playerHealth;

    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;
    [SerializeField]
    private float damageTimer;

    [SerializeField]
    public bool  isDashing,
                 canDash,
                 canTakeDamage;

    public PlayerWeapon weapon;

    private void OnEnable()
    {
        ApplyItems();
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.audSource.clip = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().clip;
            soundManager.audSource.Play();
        }

    }

    void Start()
    {
        canTakeDamage = true;
        damageTimer = 1f;
        anim.SetFloat("moveY", -1);

        if (sceneLoader == null)
            sceneLoader = FindObjectOfType<SceneLoader>();

        if (sceneLoader != null)
        {
            if (!SceneManager.GetSceneByName("UI").isLoaded)
                sceneLoader.LoadSceneAdd("UI");
        }

        playerData = Resources.Load<PlayerData>("Prefabs/PlayerData");
        if (playerData.isPersist)
            playerData.LoadItems(inventory.items);
        else
            playerData.items.Clear();

        playerHealth = FindObjectOfType<PlayerHealth>();
        health = playerHealth.startingHealth;

        activeMoveSpeed = movementSpeed;
        isDashing = false;
        canDash = true;
        dashSpeed = 10;
        damage = 2.5f;
        isDead = false;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        hitBoxes.Add("rightHB", transform.GetChild(0).gameObject);
        hitBoxes.Add("leftHB", transform.GetChild(1).gameObject);
        hitBoxes.Add("upHB", transform.GetChild(2).gameObject);
        hitBoxes.Add("downHB", transform.GetChild(3).gameObject);


    }

    void Update()
    {
        if (!canTakeDamage)
        {
            damageTimer -= Time.deltaTime;
        }

        if (damageTimer <= 0)
        {
            canTakeDamage = true;
            damageTimer = 1;
        }
        /*
        if (!soundManager.audSource.isPlaying)
        {
            soundManager.audSource.clip = (AudioClip)Resources.Load("Audio/BGM/AstralForm");
            soundManager.audSource.Play();
        }
          */

        health = playerHealth.currentHealth;
        if (health <= 0)
            Death();

        Shoot();
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
            StartCoroutine(Dash());

        if (isDashing)
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
            Physics2D.IgnoreLayerCollision(9, 7, true);
        }

        else
        {
            Physics2D.IgnoreLayerCollision(6, 7, false);
            Physics2D.IgnoreLayerCollision(9, 7, false);
        }

        if (GameObject.FindGameObjectWithTag("Boss"))
        {
            soundManager = FindObjectOfType<SoundManager>();
            soundManager.audSource.clip = (AudioClip)Resources.Load("Audio/BGM/Boss");
            soundManager.audSource.Play();
        }

    }

    public void Death()
    {

        sceneLoader.LoadScene("GameOver");

    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        canMove = false;

        if (isWalking)
            rb.velocity = new Vector2(moveDir.x, moveDir.y).normalized * dashSpeed;
        else
            rb.velocity = new Vector2(weapon.dir.x, weapon.dir.y).normalized * dashSpeed;


        yield return new WaitForSeconds(dashLength);
        canMove = true;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("isDashing");
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                isDashing = true;
            }

        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = movementSpeed;
                dashCoolCounter = dashCooldown;
                isDashing = false;
            }
        }

        if (dashCoolCounter > 0)
            dashCoolCounter -= Time.deltaTime;
        */
    }

    private void ApplyItems()
    {
        foreach (GameObject item in inventory.items)
        {
            if (item.name == "PE Book")
            {
                damage += 2.5f;
                Debug.Log(item.name + " stats applied!");
            }

            if (item.name == "Health Book")
            {
                playerHealth = FindObjectOfType<PlayerHealth>();
                playerHealth.startingHealth += 1f;
                Debug.Log(item.name + " stats applied!");
            }

            if (item.name == "History Book")
            {
                moveSpeed += 25f;
                Debug.Log(item.name + " stats applied!");
            }
        }
    }


    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
            weapon.Fire();
    }



    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack");
            StartCoroutine(StartAttack());
        }
        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isAttacking", false);
        }
    }


    IEnumerator StartAttack()
    {
        // Get mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction = direction.normalized;
        isAttacking = true;
        anim.SetFloat("mouseX", direction.x);
        anim.SetFloat("mouseY", direction.y);
        anim.SetBool("isAttacking", isAttacking);
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }


    #region Colliders/Triggers
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing)
        {
            if (collision.gameObject.tag == "Enemy")
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
        }
        else
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, false);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTakeDamage)
        {
            if (collision.gameObject.tag == "EnemyBullet")
            {
                Debug.Log("DAMAGED");
                playerHealth.TakeDamage(collision.gameObject.GetComponent<EnemyBullet>().damage);
                canTakeDamage = false;
            }
        }
     
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDashing)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
                canTakeDamage = false;
            }
               
        }
        else
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, false);
    }
    #endregion
}
