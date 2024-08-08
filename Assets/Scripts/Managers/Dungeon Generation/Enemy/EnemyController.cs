using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die
};

public class EnemyController : MonoBehaviour
{
    GameObject player;
    public EnemyState currState = EnemyState.Idle;

    public float maxHealth = 10;
    public float currHealth;
    public float damage = 0.25f;
    public float followRange;
    public float speed;

    private bool chooseDir = false;
    private bool dead = false;
    private Vector3 randomDir;

    public bool notInRoom = true;

    public EnemyHealthBar healthBar;

    float timeBtwShots;
    public float startTimeBtwShots;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;

        healthBar.SetHealth(currHealth, maxHealth);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                Death();
                break;
        }

        if (!notInRoom)
        {
            if (IsPlayerInRange(followRange) && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(followRange) && currState != EnemyState.Die)
            {
                currState = EnemyState.Wander;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float _followRange)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= _followRange;
    }
    private bool IsPlayerInAttackRange(float _attckRange)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= _attckRange;
    }

    IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Idle()
    {
        CancelInvoke();
    }

    void Wander()
    {
        CancelInvoke();
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += transform.right * speed * Time.deltaTime;

        if (IsPlayerInRange(followRange))
        {
            currState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        CancelInvoke();

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(float _damage)
    {
        currHealth -= _damage;
        healthBar.SetHealth(currHealth, maxHealth);
        Debug.Log("Enemy: I am hit");
        if (currHealth <= 0)
        {
            currState = EnemyState.Die;
        }
    }

    public void Death()
    {
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
        CancelInvoke();
        Destroy(gameObject);

        if (gameObject.tag == "Boss")
        {
            SceneLoader sceneLoader;
            sceneLoader = FindObjectOfType<SceneLoader>();
            PlayerData data = Resources.Load<PlayerData>("Prefabs/PlayerData");

            if (data.first)
            {
                data.ResetYears();
                data.second = true;
                sceneLoader.LoadScene("Game");
            }

            else if (data.second)
            {
                data.ResetYears();
                data.third = true;
                sceneLoader.LoadScene("GamePoor");
            }

            else if (data.third)
            {
                data.ResetYears();
                data.fourth = true;
                sceneLoader.LoadScene("GamePoor");
            }

            else if (data.fourth)
            {
                data.ResetYears();
                data.graduate = true;
                sceneLoader.LoadScene("GameWon");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            Debug.Log("Take Damage");
            PlayerAstralForm astral;
            astral = FindObjectOfType<PlayerAstralForm>();

            TakeDamage(astral.damage);
        }
    }
}
