using UnityEngine;

public class EnemyBulletFire : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;
    private Transform player;

    private bool invoked = false;

    private void Start()
    {
        player = GameObject.Find("PlayerAstralForm").transform;
    }

    public void Fire()
    {
        Debug.Log("This test");
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;
        Vector2 bulDir;
        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = 0;
            float bulDirY = 0;
            if (player.position.y > transform.position.y)
            {
                bulDirX = transform.position.x - Mathf.Sin((angle * Mathf.PI) / 180f);
                bulDirY = transform.position.y - Mathf.Cos((angle * Mathf.PI) / 180f);
            }
            else
            {
                bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            }

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);

            bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = GetComponentInChildren<BulletPool>().GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<EnemyBullet>().SetMoveDirection(bulDir);

            angle += angleStep;

            if (!invoked)
            {
                InvokeRepeating("Fire", 0f, 2f);
                invoked = true;
            }
        }
    }
}
