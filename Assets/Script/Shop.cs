using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint[] towers;

    public void SelectTower(int index)
    {
        if (index >= 0 && index < towers.Length)
        {
            // --- แก้ไขจุดนี้: เรียกเสียงโดยใช้ชื่อ String ---
            // หมายเหตุ: ชื่อ "Click" ต้องตรงกับที่ตั้งไว้ใน Inspector ของ SoundManager
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySound("Click");
            }

            BuildManager.instance.SelectTowerToBuild(towers[index]);
            Debug.Log("เลือกป้อมลำดับที่: " + index);
        }
        else
        {
            Debug.LogError("ไม่พบป้อมในลำดับที่ " + index);
        }
    }
}