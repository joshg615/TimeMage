using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float speed = 10f;
    public float slowdownRate = 0.5f; // adjust this value to change how quickly the bomb slows down
    public float blastRadius = 5f; // The radius of the explosion
    public float explosionForce = 10f; // The force of the explosion
    public GameObject explosionEffect; // The effect that plays when the bomb explodes
    public float countDown = 3f;
    public float blinkDelay = 0.2f;
    public SpriteRenderer bombRenderer;
    Animator animator;

    private Rigidbody2D bombRb;

    void Start()
    {
        bombRb = GetComponent<Rigidbody2D>();
        bombRenderer = GetComponent<SpriteRenderer>();    
        animator = GetComponent<Animator>();
        StartCoroutine(ExplodeAfterDelay());
    }

    void Update()
    {
        if (speed > 0f)
        {
            speed -= slowdownRate * Time.deltaTime;
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0;//Camera.main.transform.position.z - transform.position.z;
            Vector2 direction = (Camera.main.ScreenToWorldPoint(mousePosition) - transform.position).normalized;

            //rb.AddForce(direction * speed, ForceMode2D.Impulse);
            bombRb.velocity = direction *  speed;
        }
       
    }
    private IEnumerator ExplodeAfterDelay()
    {
        for (float i = 0; i <= countDown; i++)
        {
            Color color = Color.white;
            Color colorRed = Color.red;
            bombRenderer.color = colorRed;
            yield return new WaitForSeconds(blinkDelay);
            bombRenderer.color = color;
            yield return new WaitForSeconds(blinkDelay);
        }
        animator.Play("Explosion");
        //yield return new WaitForSeconds(delay);
        Explode();
    }
    private void Explode()
    {

        // Find all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        
        foreach (Collider2D col in colliders)
        {
            // Calculate the distance from the bomb to the collider
            Vector2 direction = col.transform.position - transform.position;
            float distance = direction.magnitude;

            
            //Apply explosion force to rigidbodies within the explosion radius
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            //animator.SetTrigger("explode");
            if (rb != null)
            {
                if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("OlderEnemy"))
                {
                    rb.AddForce(direction.normalized * explosionForce * (1f - distance / blastRadius), ForceMode2D.Impulse);
                }
                //rb.velocity= Vector2.zero;
            }

            // Apply damage to health component of gameobjects within the explosion radius
            Enemy health = col.GetComponent<Enemy>();
            if (health != null)
            {
               health.TakeDamage(10);
               rb.velocity = Vector2.zero;
            }
        }

        // Destroy the bomb gameobject
        //Destroy(explosion);
       
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the bomb radius in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
    //private void OnCollisionEnter(Collision collision)
    //{

    //    // Play the explosion effect
    //    Instantiate(explosionEffect, transform.position, Quaternion.identity);

    //    // Get all the objects within the blast radius
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

    //    // Damage all the objects within the blast radius
    //    foreach (Collider collider in colliders)
    //    {
    //        if (collider.CompareTag("Enemy"))
    //        {
    //            // Damage the enemy
    //            collider.GetComponent<Enemy>().TakeDamage(10);

    //            // Apply explosion force to the enemy
    //            Rigidbody enemyRb = collider.GetComponent<Rigidbody>();
    //            if (enemyRb != null)
    //            {
    //                enemyRb.AddExplosionForce(explosionForce, transform.position, blastRadius);
    //            }
    //        }
    //    }

    //    // Destroy the bomb
    //    Destroy(explosionEffect);
    //    Destroy(gameObject);
        
    //}
}
