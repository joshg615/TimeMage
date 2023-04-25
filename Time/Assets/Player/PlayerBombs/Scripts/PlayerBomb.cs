using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    public GameObject bombPrefab;  // Prefab of the bomb object to be thrown
    public GameObject ageBombPrfab;
    private GameObject currentBombPrefab;
    public float throwForce = 10f; // The force at which the bomb is thrown
    public float cooldownTime = 2f; // The amount of time before the player can throw another bomb
    private bool canThrow = true; // Whether the player can throw a bomb or not
    // Start is called before the first frame update
    void Start()
    {
        currentBombPrefab = bombPrefab;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentBombPrefab == bombPrefab)
            {
                currentBombPrefab = ageBombPrfab;
            }
            else
            {
                currentBombPrefab = bombPrefab;
            }
        }
        if (Input.GetMouseButtonDown(1) && canThrow)
        {
            // Calculate the direction of the throw based on the mouse position
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; // Distance from the camera to the object
            GameObject bomb = Instantiate(currentBombPrefab, transform.position, Quaternion.identity);
            Vector3 direction = Camera.main.ScreenToWorldPoint(mousePosition - transform.position).normalized;
            //direction.Normalize();

            // Apply force to the bomb in the direction of the throw
            bomb.GetComponent<Rigidbody2D>().velocity = direction * throwForce;

            // Set the cooldown timer
            canThrow = false;
            Invoke("ResetCooldown", cooldownTime);
        }


    }
     

    // This function resets the cooldown timer
    private void ResetCooldown()
    {
        canThrow = true;
    }
}
