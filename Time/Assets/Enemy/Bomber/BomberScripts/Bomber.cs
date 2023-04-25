using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public float speed = 3f;
    public float bombThrowDistance = 5f;
    public float throwingForce = 10f;
    public float throwingCooldown;
    private float lastThrowTime;

    private Transform playerTransform;
    private bool canThrowBomb = false;

    public GameObject bombPrefab;
    public GameObject exclamationPointPrefab;
    private GameObject exclamationPoint;
    Rigidbody2D rb;
    float slowDownRate = .05f;

    void Start()
    {
        // Set the target to the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastThrowTime = Time.time - throwingCooldown; // Set initial last throw time to avoid immediate throwing.
        rb= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > bombThrowDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            canThrowBomb = false;
        }
        else
        {
            if (Time.time - lastThrowTime >= throwingCooldown && !canThrowBomb)
            {
                // instantiate the exclamation point prefab above the bomber
                exclamationPoint = Instantiate(exclamationPointPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
                exclamationPoint.transform.SetParent(transform);
                // remove the exclamation point
                StartCoroutine(RemoveExclamationPoint());
                StartCoroutine(ThrowBomb());
                lastThrowTime = Time.time;
            }
        }
        //float bombDistanceToPlayer = Vector2.Distance(bomb.transform.position, player.position);
        //if (distanceToPlayer <= 0.5f)
        //{
        //    // Stop the bomb's velocity and explode
        //    rb.velocity = Vector2.zero;
        //    Explode();
        //}

        // move normally
        if (Mathf.Abs(rb.velocity.x) > speed)
        {
            rb.velocity *= slowDownRate;
        }
        if (Mathf.Abs(rb.velocity.y) > speed)
        {
            rb.velocity *= slowDownRate;
        }
    }

    private IEnumerator RemoveExclamationPoint()
    {
        yield return new WaitForSeconds(1f); // wait for 1 second
        Destroy(exclamationPoint);
    }


    IEnumerator ThrowBomb()
    {
        canThrowBomb = true;

        yield return new WaitForSeconds(1f);

        Vector2 throwDirection = (playerTransform.position - transform.position).normalized;
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
        bombRb.AddForce(throwDirection * throwingForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);

        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, bombRadius);
        //foreach (Collider2D collider in colliders)
        //{
        //    if (collider.CompareTag("Player"))
        //    {
        //        PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
        //        if (playerHealth != null)
        //        {
        //            playerHealth.TakeDamage(10);
        //        }
        //    }
        //}

        //Destroy(bomb);
        canThrowBomb = false;
    }
    private void ActivateTimeDilationShield(float slowdownFactor)
    {
        speed *= slowdownFactor;
    }
    private void StopTimeDilationShield(float speedUpFator)
    {
        speed /= speedUpFator;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the enemy collided with another enemy and adjust position
        if (collision.CompareTag("Enemy"))
        {
            Vector2 direction = transform.position - collision.gameObject.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            if (collision.CompareTag("TimeDilationShield"))
            {
                //isSlowed = true;
                ActivateTimeDilationShield(collision.GetComponent<TimeDilationShield>().slowdownFactor);
                Debug.Log("I have entered the time dilation");
            }
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
