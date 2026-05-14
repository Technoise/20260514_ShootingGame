using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
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
