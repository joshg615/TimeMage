using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCloneSkill : MonoBehaviour
{
    public GameObject timeClonePrefab; // Prefab for the time clone
    public float timeCloneDuration = 6f; // Duration of the time clone in seconds
    public float timeCloneCooldown = 3f; // Cooldown time for using the skill again in seconds
    MagicScript magicScript;
    private bool canUseSkill = true; // Flag to check if the skill can be used again


    private void Start()
    {
        magicScript = GetComponent<MagicScript>();
    }
    // Update is called once per frame
    void Update()
    {
        // Check if the skill can be used and if the player presses the designated button (e.g. "Q")
        if (canUseSkill && Input.GetKeyDown(KeyCode.F))
        {
            // Instantiate the time clone prefab at the player's position and rotation
            GameObject timeClone = Instantiate(timeClonePrefab, transform.position, transform.rotation);

            // Set the time clone's position to the player's position at a designated time in the past (e.g. 2 seconds)
            //timeClone.transform.position = transform.position * 2f;
            timeClone.transform.position = magicScript.positions[0];

            // Set the time clone's rotation to the player's rotation at the designated time in the past
            

            // Start a coroutine to destroy the time clone after a designated duration (e.g. 5 seconds)
            StartCoroutine(DestroyTimeClone(timeClone, timeCloneDuration));

            // Set the flag to false so the skill can't be used again until the cooldown time has passed
            canUseSkill = false;

            // Start a coroutine to reset the flag after the designated cooldown time has passed
            StartCoroutine(ResetCanUseSkill(timeCloneCooldown));
        }
    }

    IEnumerator DestroyTimeClone(GameObject timeClone, float duration)
    {
        // Wait for the designated duration
        yield return new WaitForSeconds(duration);

        // Destroy the time clone
        Destroy(timeClone);
    }

    IEnumerator ResetCanUseSkill(float cooldown)
    {
        // Wait for the designated cooldown time
        yield return new WaitForSeconds(cooldown);

        // Reset the flag so the skill can be used again
        canUseSkill = true;
    }
}

