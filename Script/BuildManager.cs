using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private static BuildManager _instance;
    public static BuildManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BuildManager>();
            }
            return _instance;
        }
    }

    public TowerBlueprint towerToBuild;

    void Awake()
    {
        _instance = this;
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
        Debug.Log("เลือกป้อม: " + tower.prefab.name + " แล้ว");
    }

    public void BuildTowerOn(Node node)
    {
        if (towerToBuild == null || towerToBuild.prefab == null)
        {
            Debug.LogError("ยังไม่ได้เลือกป้อมที่จะสร้าง!");
            return;
        }

        // ตรวจสอบ GameManager และเช็คเงิน
        if (GameManager.instance != null && GameManager.instance.gold >= towerToBuild.cost)
        {
            GameManager.instance.SpendGold(towerToBuild.cost);
            GameObject newTower = Instantiate(towerToBuild.prefab, node.transform.position, Quaternion.identity);
            node.SetTower(newTower);

            // --- แก้ไข: เรียกเสียงด้วยชื่อ String ---
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound("Build");

            Debug.Log("สร้างป้อมสำเร็จ!");
        }
        else
        {
            // --- แก้ไข: เรียกเสียงด้วยชื่อ String ---
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound("ZombieHit");

            Debug.Log("สร้างไม่ได้: เงินไม่พอ หรือ GameManager หายไป!");
        }
    }
}