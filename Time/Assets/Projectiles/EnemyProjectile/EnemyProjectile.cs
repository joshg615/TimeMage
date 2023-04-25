using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
    //        if (playerHealth != null)
    //        {
    //            playerHealth.TakeDamage(3);
    //        }
    //        Destroy(gameObject);
    //    }


    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Collider")
        {
            // Handle collision here
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = col.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(3);
            }
            Destroy(gameObject);
        }
    }
}
