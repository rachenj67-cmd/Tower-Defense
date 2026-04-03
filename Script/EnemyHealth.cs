using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3; 
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

     
        Debug.Log("เลือดซอมบี้ลดเหลือ: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
       
        Destroy(gameObject);

       
    }
}