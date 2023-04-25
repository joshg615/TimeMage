using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionForce = 50f;
    public float delay = 5f;
    public float blinkDelay = 0.2f;
    public SpriteRenderer bombRenderer;

    private bool hasExploded = false;
    private Transform player;
    private Rigidbody2D rb;
    public float slowdownFactor = 0.10f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        bombRenderer= rb.GetComponent<SpriteRenderer>();
        StartCoroutine(ExplodeAfterDelay());
    }
    private void Update()
    {
        //float bombDistanceToPlayer = Vector2.Distance(transform.position, player.position);
        //if (bombDistanceToPlayer <= 0.5f)
        //{
        //    // Stop the bomb's velocity and explode
        //    rb.velocity = Vector2.zero;
        //    //Explode();
        //}
       
    }
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0.1f) // Check if the bomb is still moving
        {
            // Slow down the velocity of the bomb
            rb.velocity *= slowdownFactor;
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        for (float i = 0; i <= delay; i += 1)
        {
            Color color= Color.white;
            Color colorRed = Color.red;
            bombRenderer.color = colorRed;
            yield return new WaitForSeconds(blinkDelay);
            bombRenderer.color = color;
            yield return new WaitForSeconds(blinkDelay);
        }
        //yield return new WaitForSeconds(delay);
        Explode();
    }

    private void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            // Calculate the distance from the bomb to the collider
            Vector2 direction = col.transform.position - transform.position;
            float distance = direction.magnitude;

            // Apply explosion force to rigidbodies within the explosion radius
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                //rb.AddForce(direction.normalized * explosionForce * (1f - distance / explosionRadius), ForceMode2D.Impulse);
                //rb.velocity= Vector2.zero;
            }

            // Apply damage to health component of gameobjects within the explosion radius
            PlayerHealth health = col.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(10);
                rb.velocity = Vector2.zero;
            }
        }

        // Destroy the bomb gameobject
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the bomb radius in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

