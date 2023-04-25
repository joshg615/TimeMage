using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilationShield : MonoBehaviour
{
    public float slowdownFactor = 0.5f;
    public float slowdownDuration = 3f;
    public float radius = 5f;

    public List<Rigidbody2D> affectedEnemies = new List<Rigidbody2D>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null && !affectedEnemies.Contains(enemyRb))
            {
                affectedEnemies.Add(enemyRb);
            }
        }
        else if (other.CompareTag("EnemyProjectile"))
        {
            Rigidbody2D projectileRb = other.GetComponent<Rigidbody2D>();
            if (projectileRb != null && !affectedEnemies.Contains(projectileRb))
            {
                affectedEnemies.Add(projectileRb);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyProjectile"))
        {
            
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null && affectedEnemies.Contains(enemyRb))
            {
                affectedEnemies.Remove(enemyRb);
            }
        }
    }

    void FixedUpdate()
    {
        foreach (Rigidbody2D enemyRb in affectedEnemies)
        {
            if (enemyRb != null)
            {
                //enemyRb.velocity *= slowdownFactor;
            }
        }
    }

    IEnumerator TimeDilationCoroutine()
    {
        float timeElapsed = 0f;
        while (timeElapsed < slowdownDuration)
        {
            yield return  new WaitForEndOfFrame();
            timeElapsed += Time.unscaledDeltaTime;
        }
        RemoveTimeDilation();
    }

    void RemoveTimeDilation()
    {
        StopAllCoroutines();
        foreach (Rigidbody2D enemyRb in affectedEnemies)
        {
            if (enemyRb != null)
            {
                enemyRb.velocity /= slowdownFactor;
            }
        }
        affectedEnemies.Clear();
    }

    public void ActivateTimeDilation()
    {
        StartCoroutine(TimeDilationCoroutine());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Start()
    {
        //ActivateTimeDilation();
        //Destroy(gameObject, 4f);
    }
}

