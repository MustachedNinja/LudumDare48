using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;
    [SerializeField] private float minSpawnDistance;
    [SerializeField] private float maxSpawnDistance;
    // [Range(0, 1)][SerializeField] private float falloff;

    private float spawnRate;
    private float elapsedTime = 0.0f;

    private int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (enemyPrefab == null) {
            Debug.LogError("EnemyPrefab not set");
        }
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }


    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > spawnRate && enemyCount < maxEnemies) {
            SpawnEnemy();
        }
    }

    private bool GetRandomSpawnPoint(Vector3 center, out Vector3 result) {
        for (int i = 0; i < 100; i++) {
            Vector3 randomPosition = Random.insideUnitSphere.normalized;
            float randomSpawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            randomPosition *= randomSpawnDistance;
            Vector3 randomPoint = center + randomPosition;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void SpawnEnemy() {
        Debug.Log("SPAWN ENEMY");
        Vector3 spawnPosition;
        bool foundSpawnPoint = GetRandomSpawnPoint(player.position, out spawnPosition);
        if (foundSpawnPoint) {
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.parent = gameObject.transform;
            Debug.Log("FOUND SPAWN POINT");
            enemyCount++;
        }
        spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        elapsedTime = 0.0f;
    }
}
