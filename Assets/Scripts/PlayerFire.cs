using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float autoFireInterval = 0.2f;

    private float autoFireTimer;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
            autoFireTimer = 0f;
        }

        if (Input.GetButton("Fire1"))
        {
            autoFireTimer += Time.deltaTime;

            if (autoFireTimer >= autoFireInterval)
            {
                FireBullet();
                autoFireTimer = 0f;
            }
        }
        else
        {
            autoFireTimer = 0f;
        }
    }

    private void FireBullet()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            return;
        }

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
