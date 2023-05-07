using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedsterScript : MonoBehaviour
{
    public float speed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public float dashMinDistance = 5f;
    public float followDistance = 10f;
    public float dragSpeed = 100f;
    public float slowDownRate = 0.5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isDashing;
    private float dashTimeLeft;
    private float dashCooldownLeft = 3;
    private Transform target;
    public Animator animator;
    public GameObject exclamationPointPrefab;
    private GameObject exclamationPoint;
    private bool isSlowed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator= this.GetComponent<Animator>();
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius);
        foreach (Collider2D col in colliders)
        {
            PlayerHealth health = col.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(4);
            }
        }
        if (!isDashing)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance <= followDistance)
            {
                // follow the player
                moveDirection = (target.position - transform.position).normalized;
            }
            //else
            //{
            //    // move randomly
            //    moveDirection = new Vector2(UnityEngine.Random.Range(-1f, 3f), UnityEngine.Random.Range(-1f, 3f)).normalized;
            //}

            // check if dash is ready
            if (dashCooldownLeft <= 0f)
            {
                // check if close enough to dash
                if (distance <= dashMinDistance)
                {
                    // instantiate the exclamation point prefab above the bomber
                    exclamationPoint = Instantiate(exclamationPointPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
                    exclamationPoint.transform.SetParent(transform);
                    // remove the exclamation point
                    StartCoroutine(RemoveExclamationPoint());
                    animator.SetBool("Dash2", true);
                    StartDash();
                }
            }
            else
            {
                dashCooldownLeft -= Time.deltaTime;
            }
        }
        else
        {
            if (dashTimeLeft <= 0f)
            {
                StopDash();

            }
            else
            {
                dashTimeLeft -= Time.deltaTime;
            }
        }
    }

    private IEnumerator RemoveExclamationPoint()
    {
        yield return new WaitForSeconds(3f); // wait for 1 second
        Destroy(exclamationPoint);
    }



    void FixedUpdate()
    {
        if (!isDashing)
        {
            // move normally
            if(Mathf.Abs(rb.velocity.x) >  speed)
            {
                rb.velocity *= slowDownRate; 
            }
            if (Mathf.Abs(rb.velocity.y) > speed)
            {
                rb.velocity *= slowDownRate;
            }
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Dash2", false);
            // dash
            rb.velocity = moveDirection * dashSpeed * speed;
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        dashCooldownLeft = dashCooldown;
    }

    void StopDash()
    {
        isDashing = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // do damage to player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
        }

        // stop dashing if hit something
        StopDash();
    }

    private void ActivateTimeDilationShield( float slowdownFactor)
    {
        speed *= slowdownFactor;
    }
    private void StopTimeDilationShield(float speedUpFator)
    {
        speed /= speedUpFator;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TimeDilationShield"))
        {
            isSlowed = true; 
            ActivateTimeDilationShield(collision.GetComponent<TimeDilationShield>().slowdownFactor);
            Debug.Log("I have entered the time dilation");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TimeDilationShield"))
        {
            isSlowed = false; 
            StopTimeDilationShield(other.GetComponent<TimeDilationShield>().slowdownFactor);
        }
    }
}
