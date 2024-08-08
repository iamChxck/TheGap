using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;


    private void Start()
    {
        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null)
            totalHealthBar.fillAmount = FindObjectOfType<PlayerAstralForm>().health / 10;
    }


    private void Update()
    {
        if (playerHealth != null)
            currentHealthBar.fillAmount = FindObjectOfType<PlayerAstralForm>().health / 10;
    }
}
