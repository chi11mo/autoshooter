using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float spawnInterval = 3f;
    public float spawnRadius = 10f;
     public float spawnHeight = 1f;
     
    private float timeSinceLastSpawn = 0f;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnEnemy()
    {
         Vector3 randomSpawnPosition = playerTransform.position + Random.insideUnitSphere * spawnRadius;
        randomSpawnPosition.y = 0f; // Set the initial Y position to ground level
        randomSpawnPosition.y += spawnHeight; // Add the desired spawn height

        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
    }
}
