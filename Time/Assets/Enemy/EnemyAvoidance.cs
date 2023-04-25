using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAvoidance : MonoBehaviour
{
    public float avoidanceRadius = 2f;
    public float avoidanceForce = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);
        Vector2 avoidanceDirection = Vector2.zero;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.gameObject != gameObject)
            {
                Vector2 direction = (transform.position - collider.transform.position).normalized;
                avoidanceDirection += direction;
            }
        }

        if (avoidanceDirection != Vector2.zero)
        {
            Vector2 avoidanceVelocity = avoidanceDirection.normalized * avoidanceForce;
            rb.velocity += avoidanceVelocity * Time.deltaTime;
        }
    }
}
