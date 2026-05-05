using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject yellowEnemyPrefab;
    public GameObject redEnemyPrefab;

    public Transform player;

    public float spawnRadius = 6f;
    public float spawnInterval = 2f;

    [Range(0f, 1f)]
    public float redSpawnChance = 0.3f;

    private bool isSpawning = true;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (!isSpawning) return;

        Vector2 spawnPos = (Vector2)player.position +
                           Random.insideUnitCircle.normalized * spawnRadius;

        GameObject prefab;

        if (Random.value < redSpawnChance)
        {
            prefab = redEnemyPrefab;
            Debug.Log("Spawned RED enemy");
        }
        else
        {
            prefab = yellowEnemyPrefab;
            Debug.Log("Spawned YELLOW enemy");
        }

        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);

        if (enemy.GetComponent<EnemyMovement>() == null)
        {
            Debug.LogError("Enemy missing EnemyMovement.cs");
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke(nameof(SpawnEnemy));
    }
}