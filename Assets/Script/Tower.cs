using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Settings")]
    public float range = 5f;            
    public float fireRate = 1f;         
    public float rotationSpeed = 10f;

    [Header("References")]
    public GameObject bulletPrefab;   
    public Transform firePoint;      

    private float fireCountdown = 0f;

    void Update()
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
        {
        
            LockOnTarget(nearestEnemy.transform);

        
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void LockOnTarget(Transform target)
    {
       
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

     
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

      
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
     
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}