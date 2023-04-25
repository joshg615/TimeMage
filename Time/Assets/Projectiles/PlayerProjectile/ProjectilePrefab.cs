using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public AudioClip shootSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(audioSource, 1f);
    }

    private void Start()
    {
        //AudioClip.Instantiate(shootSound);
        if (audioSource == null)
        {
            audioSource.clip = shootSound;
        }
        //audioSource.PlayOneShot(shootSound);
        // Play the shoot sound
        audioSource.PlayOneShot(shootSound, 0.5f);
        //Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("OlderEnemy"))
        {
            // Apply damage to the enemy or other effects
            other.gameObject.GetComponent<Enemy>().TakeDamage(5);
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}
