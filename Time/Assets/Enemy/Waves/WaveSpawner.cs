using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab of enemy to spawn
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3; 
    public float spawnDelay = 60f; // Delay between spawns
    public int enemiesPerWave = 5; // Number of enemies to spawn in each wave
    public int totalWaves = 3; // Total number of waves
    public float spawnRadius = 5f; // Radius around camera to spawn enemies
    public Camera mainCamera; // Main camera

    private int waveCount = 0; // Current wave count
    public List<GameObject> spawnedEnemies = new List<GameObject>(); // List of spawned enemies
    private bool isSpawning = false; // Flag to check if currently spawning enemies
    public float waveDuration = 0;
    Animator animator;
    Wave currentWave; 
    public struct Wave
    {
        public int enemyCount1;
        public int enemyCount2;
        public float spawnInterval;
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (!isSpawning && waveCount < totalWaves && waveDuration <= 0)
        {
            StartCoroutine(SpawnWave());
        }
        

        if(waveDuration > 0)
        { waveDuration -= Time.deltaTime; }
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        waveCount++;
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 camerPosition = mainCamera.transform.position;
            camerPosition.z = 0;
            Vector3 spawnWorldPos = camerPosition + new Vector3(spawnPos.x, spawnPos.y, 0f);
            Vector2 spawnPos2 = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnWorldPos2 = camerPosition + new Vector3(spawnPos2.x, spawnPos2.y, 0f);
            
            GameObject newEnemy = Instantiate(enemyPrefab, spawnWorldPos, Quaternion.identity);
            animator = newEnemy.GetComponent<Animator>(); // get the Animator component on the enemy prefab
            //animator.SetTrigger("Spawn"); // trigger the spawn animation
            animator.SetTrigger("Smoke"); // trigger the spawn animation
            StartCoroutine(WaitForAnimation(animator));
            //animator.ResetTrigger("Smoke");
            GameObject secondEnemy = Instantiate(enemyPrefab2, spawnWorldPos2, Quaternion.identity);
            animator = secondEnemy.GetComponent<Animator>();
            StartCoroutine(WaitForAnimation(animator));
            spawnedEnemies.Add(newEnemy);
            spawnedEnemies.Add(secondEnemy);
            yield return new WaitForSeconds(spawnDelay);
        }
        waveDuration = 30f;
        isSpawning = false;
    }

    IEnumerator WaitForAnimation(Animator animator)
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // Reset the trigger to false
        animator.ResetTrigger("Smoke");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
