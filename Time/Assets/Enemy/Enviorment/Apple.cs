using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Apple : MonoBehaviour
{
    public int healthToAdd = 10; // the amount of health to add when the player collides with the apple

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.AddHealth(healthToAdd);
                Destroy(gameObject); // destroy the apple object once the player collides with it
            }
        }
    }
}

