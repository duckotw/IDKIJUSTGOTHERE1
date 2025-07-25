using System.Collections;
using UnityEngine;

// This is a monster spawner script, it behaves like the mob spawner in Minecraft - Gatsby

public class MonsterSpawner : MonoBehaviour
 {
    public GameObject enemyPrefab;                // Enemy prefab to spawn

    
    public int minEnemiesPerBatch = 3;            // Minimum number of enemies per batch
    public int maxEnemiesPerBatch = 6;            // Maximum number of enemies per batch

    
    public float minSpawnInterval = 3f;           // Minimum interval between spawns
    public float maxSpawnInterval = 5f;           // Maximum interval between spawns
    
    public float spawnBatchRadius = 2f;           // Radius within which enemies will be spawned in each batch

    public void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator SpawnEnemies()
    {
        float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
        yield return new WaitForSeconds(interval);

        int enemiesToSpawn = Random.Range(minEnemiesPerBatch, maxEnemiesPerBatch);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector2 spawnOffset = Random.insideUnitCircle * spawnBatchRadius;
            Vector2 spawnPosition = (Vector2)gameObject.transform.position + spawnOffset;

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}


