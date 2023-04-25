using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float projectileCooldown = 1f;
    public float attackRange = 10f;
    public float moveSpeed = 3f;
    public float stoppingDistance = 2f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private float projectileTimer = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if player is within attack range
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            // Stop moving if within stopping distance
            if (distanceToPlayer <= stoppingDistance)
            {
                rb.velocity = Vector2.zero;
            }
            else // Move towards player
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
            }

            // Shoot projectiles
            if (projectileTimer <= 0f)
            {
                //anim.SetTrigger("Shoot");
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Vector2 shootDirection = (player.position - transform.position).normalized;
                projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;
                projectileTimer = projectileCooldown;
            }
            else
            {
                projectileTimer -= Time.deltaTime;
            }
        }
        else // Move towards player if out of range
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    private void ActivateTimeDilationShield(float slowdownFactor)
    {
        moveSpeed *= slowdownFactor;
    }
    private void StopTimeDilationShield(float speedUpFator)
    {
        moveSpeed /= speedUpFator;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TimeDilationShield"))
        {
            //isSlowed = true;
            ActivateTimeDilationShield(collision.GetComponent<TimeDilationShield>().slowdownFactor);
            Debug.Log("I have entered the time dilation");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TimeDilationShield"))
        {
            //isSlowed = false;
            StopTimeDilationShield(other.GetComponent<TimeDilationShield>().slowdownFactor);
        }
    }
}
