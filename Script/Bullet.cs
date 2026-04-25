using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Bullet")) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth == null)
                enemyHealth = other.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);

                // --- แก้ไขตรงนี้ครับ ---
                // เรียกชื่อเสียงตามที่คุณตั้งไว้ใน List ของ SoundManager
                if (SoundManager.instance != null)
                {
                    SoundManager.instance.PlaySound("ZombieHit");
                }
                // ---------------------

                Debug.Log("สั่งลดเลือดสำเร็จ! และเล่นเสียงแล้ว");
            }
            else
            {
                Debug.Log("ERROR: หาไฟล์ EnemyHealth บนตัวซอมบี้ไม่เจอ!");
            }

            Destroy(gameObject);
        }
    }
}