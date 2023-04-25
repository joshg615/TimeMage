using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(other.gameObject.CompareTag("Collider"))
        {
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Collider")
        {
            // Handle collision here
            Destroy(gameObject);
        }
    }
}
