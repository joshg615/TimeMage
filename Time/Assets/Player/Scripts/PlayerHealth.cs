
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float damageCoolDown = 5f; // 1 second cooldown
    private float lastDamageTime;
    public float blinkDelay = 0.2f;
    public GameObject blinkObject;
    private bool isInvulnerable = false;
    public Renderer playerRenderer;
    public static PlayerHealth instance;
    public Image healthBar;



    void Start()
    {
        currentHealth = maxHealth;
        playerRenderer= GetComponent<Renderer>();
        instance = this;
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(DamageDelay());
            }
        }
    }

    public void AddHealth( float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetHealth( float health)
    {
        
        currentHealth = health; 
    }
    IEnumerator DamageDelay()
    {
        isInvulnerable = true;

        for (float i = 0; i < damageCoolDown; i += 1)
        {
            playerRenderer.enabled= false;
            yield return new WaitForSeconds(blinkDelay);
            playerRenderer.enabled= true;   
            yield return new WaitForSeconds(blinkDelay);
        }

        isInvulnerable = false;
    }

    public bool IsFullHealth()
    {
        if(currentHealth != maxHealth)
        {
            return false;
        }
        return true;
        
    }

    public float CurrentHealth()
    {
        return currentHealth;
    }
    void Die()
    {
        // Here you can add the code to handle the player's death
        // Set a trigger parameter in the Animator to play the death animation
        //animator.SetTrigger("Die");

        // Start a coroutine to delay loading the game over scene
        StartCoroutine(LoadGameOverScene());
    }

    IEnumerator LoadGameOverScene()
    {
        // Wait for a few seconds before loading the game over scene
        yield return new WaitForSeconds(3);

        // Load the game over scene
        SceneManager.LoadScene("Game Over");
    }
}

