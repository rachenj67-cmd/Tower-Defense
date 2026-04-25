using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject towerOnNode;

    void Update()
    {
        // ถ้ามีการกดเมาส์ซ้าย
        if (Input.GetMouseButtonDown(0))
        {
            // แปลงตำแหน่งเมาส์บนหน้าจอ ให้เป็นตำแหน่งในโลกของเกม (World Space)
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ยิง Raycast ออกไปเช็คว่าโดน Collider ของ Node ตัวนี้ไหม
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // ถ้าโดนแล้ว!
                Debug.Log("คลิกโดน Node นี้แล้ว!");

                if (towerOnNode != null)
                {
                    Debug.Log("มีป้อมอยู่แล้ว!");
                    return;
                }

                BuildManager.instance.BuildTowerOn(this);
            }
        }
    }

    public void SetTower(GameObject tower)
    {
        towerOnNode = tower;
    }
}
