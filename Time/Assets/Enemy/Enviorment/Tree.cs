using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public class Tree : MonoBehaviour
{
    public GameObject oldTreePrefab; // Old tree prefab to instantiate
    public GameObject applePrefab; // Apple prefab to drop
    public GameObject clockPrefab; 
    public float dropRadius = 2f; // Radius within which apples are dropped
    public float healthGain = 10f; // Amount of health gained per apple
    public float timeToAge = 10f; // Time it takes for the tree to age

    private float ageTimer = 0f; // Timer for aging the tree
    private bool isOld = false; // Flag for whether the tree is old
    Animator animator;

    private void Start()
    { 
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
        // If the tree is aging, increment the timer
        if (ageTimer > 0f)
        {
            ageTimer -= Time.deltaTime;

            // If the timer has elapsed, age the tree
            if (ageTimer <= 0f)
            {
                animator.SetTrigger("Clock");
                AgeTree();
            }
        }

        // If the player is near the tree and presses X, age the tree
        if (Input.GetKeyDown(KeyCode.X) && Vector3.Distance(transform.position,
            PlayerHealth.instance.transform.position) < dropRadius)
        {
            if (!isOld)
            {
                StartCoroutine(Clock());
                AgeTree();
            }

            DropApples();
        }

    //    // If the player is rewinding time, reset the age timer and regain health
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        PlayerController.instance.StartRewind();
    //        ageTimer = 0f;

    //        if (PlayerController.instance.currentHealth < PlayerController.instance.maxHealth)
    //        {
    //            PlayerController.instance.currentHealth = PlayerController.instance.maxHealth;
    //        }
    //    }
    }
    IEnumerator Clock()
    {
        Instantiate(clockPrefab, transform.position, transform.rotation, transform.parent);
        yield return new WaitForSeconds(timeToAge);
        
    }

    private void AgeTree()
    {
        isOld = true;
        Instantiate(oldTreePrefab, transform.position, transform.rotation, transform.parent); 
        //Destroy(clockPrefab.gameObject);
        Destroy(gameObject);
    }

    private void DropApples()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 dropPos = transform.position + Random.insideUnitSphere * dropRadius;
            Instantiate(applePrefab, dropPos, Quaternion.identity);
        }
    }
}

