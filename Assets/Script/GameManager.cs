using UnityEngine;
using TMPro; // ใช้ TextMeshPro
using UnityEngine.SceneManagement; // ใช้สำหรับการ Restart ฉาก

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gold = 50;
    public int lives = 20; // เพิ่มตัวแปรเลือด

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI livesText; // เพิ่มตัวแปรแสดงเลือด
    public GameObject gameOverUI; // ลาก Panel Game Over มาใส่ใน Inspector

    void Awake() { instance = this; }

    void Start()
    {
        UpdateUI();
        gameOverUI.SetActive(false); // ซ่อนจอ Game Over ตอนเริ่ม
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    // --- ส่วนที่เพิ่มเข้ามาสำหรับ Game Loop ---
    public void TakeDamage(int damage)
    {
        lives -= damage;
        UpdateUI();

        if (lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameOverUI.SetActive(true); // โชว์หน้าจอ Game Over
        Time.timeScale = 0f; // หยุดเวลาในเกมทั้งหมด
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // กลับมาเดินเวลาปกติ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        goldText.text = "Gold: " + gold.ToString();
        livesText.text = "Lives: " + lives.ToString();
    }
}