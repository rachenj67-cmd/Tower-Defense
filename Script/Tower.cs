using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Settings")]
    public float range = 3f;           // ระยะยิง/ตี
    public float fireRate = 1f;        // ความเร็วในการโจมตี (ครั้งต่อวินาที)

    // --- ส่วนที่ปรับปรุงใหม่สำหรับอนิเมชั่นและ Melee ---
    [Header("Tower Type & Animation")]
    public bool isMelee = false;       // ติ๊กถูกถ้าเป็นป้อมตีใกล้ (ไม่ต้องใช้กระสุน)
    public int meleeDamage = 1;        // ความแรงตอนตีใกล้
    public Animator anim;              // <--- สำคัญ! ลาก Animator ของตัวละครมาใส่ตรงนี้ใน Inspector

    [Header("References (Ranged Only)")]
    public GameObject bulletPrefab;    // กระสุน (ใช้เมื่อ isMelee เป็น false)
    public Transform firePoint;       // จุดปล่อยกระสุน

    [Header("Audio")]
    public string attackSoundName;     // ชื่อเสียงที่จะเล่นตอนโจมตี

    private float fireCountdown = 0f;
    private Transform target;

    void Update()
    {
        UpdateTarget();

        if (target != null)
        {
            if (fireCountdown <= 0f)
            {
                Attack(); // สั่งโจมตี (รวมทั้งยิงและตีใกล้)
                fireCountdown = 1f / fireRate; // ตั้งเวลาถอยหลังสำหรับนัดถัดไป
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    // ฟังก์ชันโจมตีหลัก
    void Attack()
    {
        if (target == null) return;

        // 1. ตรวจสอบและสั่งเล่นอนิเมชั่น (ทำก่อนทั้งตีใกล้และยิง)
        if (anim != null)
        {
            // สั่ง Trigger ที่ชื่อ "Attack" ใน Animator
            // **ต้องไปสร้าง Trigger ชื่อนี้ในหน้าต่าง Animator ด้วยนะครับ**
            anim.SetTrigger("Attack");
        }

        // 2. จัดการเรื่องเสียง
        if (SoundManager.instance != null && !string.IsNullOrEmpty(attackSoundName))
        {
            SoundManager.instance.PlaySound(attackSoundName);
        }

        // 3. จัดการดาเมจตามประเภทของป้อม
        if (isMelee)
        {
            // --- ถ้าเป็นป้อมตีใกล้ (Melee) ---
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            // เผื่อ Script EnemyHealth อยู่ที่วัตถุแม่
            if (enemyHealth == null)
                enemyHealth = target.GetComponentInParent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // ลดเลือดศัตรูทันที
                enemyHealth.TakeDamage(meleeDamage);
                Debug.Log(gameObject.name + " ตีศัตรูระยะใกล้!");
            }
        }
        else
        {
            // --- ถ้าเป็นป้อมยิง (Ranged) ---
            if (bulletPrefab != null && firePoint != null)
            {
                // คำนวณมุมและสร้างกระสุน
                Vector2 direction = (Vector2)target.position - (Vector2)firePoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

                Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            }
        }
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}