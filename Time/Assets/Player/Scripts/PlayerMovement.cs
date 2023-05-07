using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 50f; // speed of the player
    Rigidbody2D rb;
    float dragSpeed  = 100f;
    public float dashSpeed = 10;
    private bool isDashing;
    public float dashTime = 4f;
    public float dashCoolDown = 4f; 
    [SerializeField] private TrailRenderer trailRenderer;

    // Update is called once per frame
    void Update()
    {
        // get horizontal and vertical input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // calculate movement vector
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized * speed * Time.deltaTime;
        movement = movement.normalized * speed * Time.deltaTime;

        //if (dashCoolDown > 0)
        //{
        //    dashCoolDown -= Time.deltaTime;

        //}
        //else
        //{
        //    isDashing= false;
        //}

        // move the player
        transform.position += movement;
        //rb.MovePosition(rb.transform.position + movement);

        //// Dash if spacebar is pressed
        //if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        //{
        //    Dash();
        //    trailRenderer.emitting = false;
        //}
    }

    private void Dash()
    {
        speed *= dashSpeed;
        isDashing= true;
        dashCoolDown = 4f; 
        trailRenderer.emitting= true;
        StartCoroutine(EndDash());
    }
    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashTime);
        speed /= dashSpeed;
        //isDashing= false;
        //trailRenderer.emitting= false;
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDashing = false;
        dashTime = 0f;
    }

    private void FixedUpdate()
    {
        float currentSpeed = rb.velocity.magnitude;
        
        rb.drag = currentSpeed * dragSpeed;
    }

}
