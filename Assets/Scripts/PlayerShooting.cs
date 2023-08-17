using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootInterval = 0.5f; // Adjust interval as needed
    public float bulletSpeed = 10f;

    private float timeSinceLastShot = 0f;

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= shootInterval)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }
    }

    private void Shoot()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10f); // Adjust radius as needed
            Debug.Log("Shooting!");

        Transform nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = collider.transform;
                }
            }
        }

        if (nearestEnemy != null)
        {
            Vector3 direction = (nearestEnemy.position - bulletSpawnPoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        }
    }
}
