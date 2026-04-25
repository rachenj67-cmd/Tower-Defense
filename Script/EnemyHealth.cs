using UnityEngine;
using System; // จำเป็นสำหรับการใช้ Action

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 100f;
    public float goldReward = 10f; // ตั้งค่าเงินแยกรายตัวได้

    private float currentHealth;
    private bool isDead = false; // กันบั๊กกรณีโดนดาเมจซ้ำตอนกำลังตาย

    // ใช้ Action เพื่อแจ้งเตือน UI หรือ Effect อื่นๆ ว่าเลือดเปลี่ยนแล้ว
    // ส่ง (currentHealth, maxHealth) ไปให้ผู้ที่สนใจ
    public event Action<float, float> OnHealthChanged;

    public void SetStats(float hp)
    {
        maxHealth = hp;
        currentHealth = maxHealth;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) // เปลี่ยนเป็น float เพื่อความแม่นยำ
    {
        if (isDead) return; // ถ้าตายแล้ว ไม่ต้องคำนวณอะไรอีก

        currentHealth -= damage;

        // แจ้งเตือนไปยัง UI (เช่น แถบเลือด)
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log($"{gameObject.name} เลือดเหลือ: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true; // ล็อกสถานะว่าตายแล้ว
        GameManager.instance.AddGold((int)goldReward);

        // ตรงนี้คุณอาจจะเพิ่ม Animation การตาย หรือ Particle Effect ก่อน Destroy
        Destroy(gameObject);
    }
}