using UnityEngine;


public class DoubleSpiral : MonoBehaviour
{
    private float angle = 0f;

    private bool invoked = false;

    public void Fire()
    {
        for(int i = 0; i <= 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);

            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<EnemyBullet>().SetMoveDirection(bulDir);
        }

        angle += 10f;

        if(angle >= 360f)
        {
            angle = 0f;
        }

        if (!invoked)
        {
            InvokeRepeating("Fire", 0f, 0.1f);
            invoked = true;
        }
    }
}
