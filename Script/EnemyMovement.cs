using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 1f;

    private Transform[] waypoints;
    private int wavepointIndex = 0;

    void Start()
    {
        GameObject waypointParent = GameObject.Find("Waypoints");
        if (waypointParent == null)
        {
            Debug.LogError("หา Object ที่ชื่อ Waypoints ไม่เจอ! เช็คชื่อใน Unity ด่วนครับ");
            return;
        }

        waypoints = new Transform[waypointParent.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointParent.transform.GetChild(i);
        }

        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        // ถ้าไม่มีทางเดิน หรือเดินจบแล้ว (wavepointIndex เกินจำนวนจุดที่มี)
        if (waypoints == null || wavepointIndex >= waypoints.Length)
        {
            // --- ตรงนี้คือตอนที่ศัตรูเดินถึงจุดหมายปลายทาง ---
            GameManager.instance.TakeDamage(1); // หักเลือดผู้เล่น
            Destroy(gameObject); // ทำลายศัตรูทิ้ง
            return;
        }

        Transform targetPos = waypoints[wavepointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);

        // เช็คระยะห่าง ถ้าใกล้ถึงจุดหมายแล้ว ให้เปลี่ยนเป้าหมายไปจุดถัดไป
        if (Vector2.Distance(transform.position, targetPos.position) < 0.1f)
        {
            wavepointIndex++;
        }
    }
}