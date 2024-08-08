using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float bulletForce;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = (GameObject)Resources.Load("Prefabs/Bullet");
        bulletForce = 20f;
    }

    private void Update()
    {
        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

    }

    private void FixedUpdate()
    {
        
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.up * bulletForce, ForceMode2D.Impulse);
    }
}
