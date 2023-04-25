using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRiftSkill : MonoBehaviour
{
    public float vacuumForce = 10f;
    public float smooth = 2f;
    public float radius = 3f;
    public float strength = 10f;
    public float damagePerSecond = 10f;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("OlderEnemy"))
            {
                Vector2 direction = (transform.position - collider.transform.position).normalized;
                Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce(direction * strength, ForceMode2D.Force);

                Enemy enemyHealth = collider.gameObject.GetComponent<Enemy>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}



