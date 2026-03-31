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
        if (waypoints == null || wavepointIndex >= waypoints.Length)
        {
            Destroy(gameObject); 
            return;
        }

       
        Vector3 targetPos = waypoints[wavepointIndex].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

       
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            wavepointIndex++;
        }
    }
}