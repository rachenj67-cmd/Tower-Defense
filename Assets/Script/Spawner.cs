using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float timeBetweenEnemies = 1f;
    public int enemiesPerWave = 5;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    void SpawnEnemy()
    {
       
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}