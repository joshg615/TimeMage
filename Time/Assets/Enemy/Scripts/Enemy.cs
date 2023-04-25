using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f; // maximum health of the enemy
    public float currentHealth = 100f; // current health of the enemy
    public GameObject agedEnemyPrefab;//aged prefab
    

    public GameObject healthBarPrefab; // reference to the health bar prefab
    public Vector3 healthBarOffset; // offset from the enemy position for the health bar

    //private GameObject healthBar; // reference to the health bar game object
    public Transform healthBarFill; // reference to the fill object of the health bar

    //public GameObject healthBar;
    public List<Vector2> enemyPositions;
    public List<Quaternion> enemyRotations;
    private Vector3 center;
    public float borderOffset = 5f;


    void Start()
    {
        
        enemyPositions = new List<Vector2>();
        enemyRotations = new List<Quaternion>();

        // Get the center of the map
        center = Camera.main.ViewportToWorldPoint(new Vector3(300f, 55f, 0));
        //currentHealth = maxHealth;
        //health.maxValue = maxHealth;
        //health.value = currentHealth;
        //agedEnemyPrefab = GetComponent<GameObject>();


        // create the health bar game object
        //healthBar = Instantiate(healthBarPrefab, this.transform.position + healthBarOffset, Quaternion.identity, transform);
        //healthBar = transform.Find("EnemyHealth").gameObject;
        //healthBarFill = healthBar.transform.Find("EnemyHealthBar");
        //healthBarPrefab.GetComponentsInChildren<>
        //healthBar.GetComponentInChildren<Image>().fillAmount; 
    }
    void Update()
    {
        // Update the position of the health bar to match the position of the enemy
        //healthBar.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        //health.value = currentHealth; 
        //healthBarFill.GetComponent<Image>().fillAmount = currentHealth; 

    }

    // apply damage to the enemy
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBarFill.GetComponent<Image>().fillAmount -= damage/100;
        // update the health bar fill
        //healthBarFill.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
        //healthBarFill.

        // check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // destroy the enemy game object
    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision was with a border
        if (collision.collider.CompareTag("Collider"))
        {
            // Calculate the direction from the enemy to the center of the map
            Vector3 direction = center - transform.position;
            direction.z = 0f;
            direction.Normalize();

            // Move the enemy in the opposite direction by a small amount
            transform.position += direction * borderOffset;
        }
    }
}
