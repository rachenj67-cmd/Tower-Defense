using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

   
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.name.Contains("Bullet")) return;

      
        Debug.Log("ชนกับ: " + other.gameObject.name);

      
        if (other.CompareTag("Enemy"))
        {
            
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            
            if (enemyHealth == null)
            {
                enemyHealth = other.GetComponentInParent<EnemyHealth>();
            }

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log("สั่งลดเลือดสำเร็จ!");
            }
            else
            {
                Debug.Log("ERROR: หาไฟล์ EnemyHealth บนตัวซอมบี้ไม่เจอ!");
            }

            Destroy(gameObject); 
        }
    }
}