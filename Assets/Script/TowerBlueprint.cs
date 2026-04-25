using UnityEngine;

[System.Serializable] // บรรทัดนี้สำคัญมาก! ถ้าไม่มีอันนี้ มันจะไม่โชว์ใน Inspector
public class TowerBlueprint
{
    public GameObject prefab; // เอาไว้ลากตัวป้อมมาใส่
    public int cost;          // เอาไว้ตั้งราคา

    // มึงสามารถเพิ่มตัวแปรอื่นได้ เช่น:
    // public int damage;
    // public float fireRate;
}
