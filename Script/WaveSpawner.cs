using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class WaveData
{
    [Header("Settings")]
    public int enemyCount = 5;

    // ลบตัวแปร enemyHealth ออกไปได้เลยครับ เพราะ Prefab จัดการตัวเองได้แล้ว
    public List<GameObject> spawnableEnemies;
}

public class WaveSpawner : MonoBehaviour
{
    [Header("References")]
    public Transform spawnPoint;
    public TextMeshProUGUI waveText;

    [Header("Wave List")]
    public List<WaveData> waves;

    [Header("Settings")]
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
            if (waveIndex < waves.Count)
            {
                StartCoroutine(SpawnWave());
            }
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        waveText.text = "Wave: " + (waveIndex + 1) + "  Next: " + Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        WaveData currentWave = waves[waveIndex];

        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            if (currentWave.spawnableEnemies.Count > 0)
            {
                int randomIndex = Random.Range(0, currentWave.spawnableEnemies.Count);
                GameObject prefabToSpawn = currentWave.spawnableEnemies[randomIndex];

                // เกิดศัตรูขึ้นมา โดยปล่อยให้ตัวมันใช้ค่าเลือดที่ตั้งไว้ใน Prefab เอง
                Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            }

            yield return new WaitForSeconds(0.5f);
        }

        waveIndex++;
    }
}