using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawn : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<Enemy> enemies;
        public float spawnRate;
    }

    public List<Wave> waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float waveSpawnMultiplier = 1.2f;

    private int waveIndex = 0;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while (true)
        {
            Wave currentWave = waves[waveIndex];

            Debug.Log("Starting Wave: " + currentWave.name);

            foreach (Enemy enemy in currentWave.enemies)
            {
                for (int i = 0; i < enemy.count; i++)
                {
                    SpawnEnemy(enemy.enemyType);
                    yield return new WaitForSeconds(currentWave.spawnRate);
                }
            }

            yield return new WaitForSeconds(timeBetweenWaves);

            waveIndex++;
            if (waveIndex >= waves.Count)
            {
                waveIndex = 0;
                timeBetweenWaves *= waveSpawnMultiplier;
            }
        }
    }

    private void SpawnEnemy(GameObject enemyType)
    {
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyType, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
    }

    [System.Serializable]
    public class Enemy
    {
        public GameObject enemyType;
        public int count;
    }
}
