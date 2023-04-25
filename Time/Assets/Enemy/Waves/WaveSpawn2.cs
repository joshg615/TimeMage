using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn2 : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemyPrefabs;
        public int count;
        public float rate;

        public Transform youngTreePrefab;
        public int treeCount;
        public float treeRate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    private int currentWave = 1; 

    public Transform[] spawnPoints;
    public Transform[] treeSpawnPoints; 

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;
    public TMP_Text waveText; 

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0f)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
        string text = string.Format("Wave " + currentWave);
        waveText.text = text;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        // Spawn enemies
        for (int i = 0; i < wave.count; i++)
        {
            Transform enemyPrefab = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];
            SpawnEnemy(enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        // Spawn young trees
        for (int i = 0; i < wave.treeCount; i++)
        {
            SpawnYoungTree(wave.youngTreePrefab);
            yield return new WaitForSeconds(1f / wave.treeRate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemyPrefab)
    {
        // Spawn enemy at random spawn point
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
    }

    void SpawnYoungTree(Transform youngTreePrefab)
    {
        // Spawn young tree at random location
        //Vector3 randomLocation = new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f));
        Transform randomSpawnPoint = treeSpawnPoints[Random.Range(0, treeSpawnPoints.Length)];
        Instantiate(youngTreePrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            // Endless waves
            nextWave = 0;
            currentWave = 1; 
        }
        else
        {
            nextWave++;
            currentWave++;
        }
    }
}
