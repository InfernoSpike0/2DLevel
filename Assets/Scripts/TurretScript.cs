using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform firePoint;

    public float fireRate = 1f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            ShootLaser();
            timer = 0f;
        }
    }

    void ShootLaser()
    {
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
    }
}
