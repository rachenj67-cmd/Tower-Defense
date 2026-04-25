using UnityEngine;
using UnityEngine.SceneManagement; // ต้องเพิ่มบรรทัดนี้เพื่อให้สลับฉากได้

public class Game : MonoBehaviour
{
    // ฟังก์ชันสำหรับปุ่ม Play
    public void PlayGame()
    {
        // ใส่ชื่อ Scene เกมของคุณในวงเล็บ (ต้องสะกดให้ตรงเป๊ะ)
        SceneManager.LoadScene("MainGame");
    }

    // ฟังก์ชันสำหรับปุ่ม Exit
    public void ExitGame()
    {
        Debug.Log("QUIT!"); // ใช้ทดสอบใน Editor
        Application.Quit(); // คำสั่งนี้จะทำงานจริงเมื่อ Build เป็นไฟล์ .exe แล้ว
    }
}