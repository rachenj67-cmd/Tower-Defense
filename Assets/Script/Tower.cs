using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Settings")]
    public float range = 3f;           
    public float fireRate = 1f;         

    [Header("References")]
    public GameObject bulletPrefab;    
    public Transform firePoint;        

    private float fireCountdown = 0f;
    private Transform target;

    void Update()
    {
        UpdateTarget();

       
        if (target != null)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null && target != null)
        {
           
            Vector2 direction = (Vector2)target.position - (Vector2)firePoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

          
            Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        }
    }

   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}