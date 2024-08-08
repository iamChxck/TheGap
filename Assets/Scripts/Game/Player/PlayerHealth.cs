
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    [SerializeField] Animator _animator;
    public float animTimer;
    private AnimationClip clip;
    public float currentHealth;

    private void Awake()
    {
        startingHealth = 5f;
    }

    private void Start()
    {
        
        currentHealth = startingHealth;
        _animator = GetComponent<Animator>();

    }

    public void UpdateAnimClipTimes()
    {
        //Get Timer of animation clips depending on animationName
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "TakeDamage":
                    animTimer = clip.length;
                    break;
               
            }
        }
    }


    IEnumerator TakeDamage()
    {
        _animator.SetBool("TakeDamage", true);
        yield return new WaitForSeconds(animTimer);
        _animator.SetBool("TakeDamage", false);
    }


    public void TakeDamage(float _damage)
    {

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if (currentHealth > 0)
        {
            //player takes damage
            StartCoroutine("TakeDamage"); 
        }

        else
        {
            //player dies
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyController>().damage);
        }

        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyBullet>().damage);
        }


    }



    public void GiveHealth(float _health)
    {
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);
    }

    private void Update()
    {
       
    }
}
