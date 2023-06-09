using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class MagicScript : MonoBehaviour
{
    public Image slowDownEffectImage;

    public float rewindDuration = 3f;
    public bool isMoving = true;


    public List<Vector2> positions;
    public List<Quaternion> rotations;
    public ClockCountDown clock;
     

    public GameObject projectilePrefab;
    public float projectileSpeed;

    public GameObject timeRiftPrefab;
    public float timeRiftDuration = 5f;
    public float timeRiftCooldown = 10f;
   
    private GameObject timeRiftPreview;
    private bool previewingRift = false;

    private bool canCreateTimeRift = true;
    private bool showLine = false;
    private LineRenderer lineRenderer;

    public GameObject timeDilation;
    

    void Start()
    {
        // Find all Enemy objects in the scene and store them in the enemies array
        // Add all initial enemies to the list
        //Enemy[] initialEnemies = FindObjectsOfType<Enemy>();
        //foreach (Enemy enemy in initialEnemies)
        //{
        //    enemies.Add(enemy);
        //}

        clock = FindObjectOfType<ClockCountDown>();

        positions = new List<Vector2>();
        rotations = new List<Quaternion>();
        //health = new List<int>();
        //currentHealth = GetComponent<PlayerHealth>().CurrentHealth();
        
    }
    private void FixedUpdate()
    {
        Record();
    }
    public void Record()
    {
        // Record the current time
        float time = Time.time;

        // Record the current position and rotation of the player
        positions.Add(this.transform.position);
        

        // Remove any old positions and rotations that are no longer needed
        while (positions.Count > 150)
        {
            positions.RemoveAt(0);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            // Create a new projectile and give it an initial velocity
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            float currentTime = Time.time;
            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //isMoving = false;
            UseRewindSkill();
            

            isMoving = true;
        }
        //if(Input.GetButtonDown("Fire3"))
        //{
        //    UseSlowDownTimeSkill();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mousePos.z = 0f;
        //    Instantiate(timeRiftPrefab, mousePos, Quaternion.identity);
        //}

        //if (Input.GetKey(KeyCode.E) && canCreateTimeRift)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        //    {
        //        Vector3 point = hit.point;
        //        point.y = transform.position.y;
        //        point.z = 0f;
        //        lineRenderer.enabled = true;
        //        lineRenderer.SetPosition(0, transform.position);
        //        lineRenderer.SetPosition(1, point);
        //        showLine = true;
        //    }
        //}

        //if (Input.GetKeyUp(KeyCode.E) && showLine)
        //{
        //    Instantiate(timeRiftPrefab, lineRenderer.GetPosition(1), Quaternion.identity);
        //    canCreateTimeRift = false;
        //    Invoke("ResetCreateRift", timeRiftCooldown);
        //    lineRenderer.enabled = false;
        //    showLine = false;
        //}

        if (Input.GetKey(KeyCode.E))
        {
            if (!previewingRift)
            {
                previewingRift = true;
                // Spawn a preview of the time rift at the current mouse position
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10f;
                Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);

                timeRiftPreview = Instantiate(timeRiftPrefab, position, Quaternion.identity);
                timeRiftPreview.GetComponent<Collider2D>().enabled = false;
                //previewingRift = true;
            }
            //else if (previewingRift)
            //{
            //    // Place the time rift at the position of the preview and enable its collider
            //    timeRiftPreview.GetComponent<Collider2D>().enabled = true;
            //    previewingRift = false;
            //}
            else
            {
                // Update the position of the time rift preview
                Vector3 mousePosition = Input.mousePosition;
                mousePosition.z = 10f;
                Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);

                timeRiftPreview.transform.position = position;
            }
        }

        if(Input.GetKeyUp(KeyCode.E))
        {
            // Place the time rift at the position of the preview and enable its collider
            timeRiftPreview.GetComponent<Collider2D>().enabled = true;
            previewingRift = false;
        }
        //else if (previewingRift)
        //{
        //    // Place the time rift at the position of the preview and enable its collider
        //    timeRiftPreview.GetComponent<Collider2D>().enabled = true;
        //    previewingRift = false;
        //}


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //timeDilation.SetActive(true);
            //Invoke("TurnOffDilation", 4f);
            TimeDialtionSkill();
        }

    }

    public void TimeDialtionSkill()
    {
        timeDilation.SetActive(true);
        Invoke("TurnOffDilation", 4f);

    }
    void ResetCreateRift()
    {
        canCreateTimeRift = true;
    }

    public void TurnOffDilation()
    {
        timeDilation.SetActive(false);
    }
    public void UseSlowDownTimeSkill()
    {
        // Slow down time for 5 seconds at half speed
        SlowDownTime(5f, 0.5f);
        // Show the slow down effect image
        slowDownEffectImage.enabled = true;
        slowDownEffectImage.color = new Color(0, 0, 0, 0.8f);
    }
    public void SlowDownTime(float duration, float timeScale)
    {
        StartCoroutine(SlowDownTimeCoroutine(duration, timeScale));
    }

    IEnumerator SlowDownTimeCoroutine(float duration, float timeScale)
    {
        // Set the time scale to the specified value
        Time.timeScale = timeScale;

        // Wait for the specified duration
        yield return new WaitForSecondsRealtime(duration);

        // Set the time scale back to 1 (normal speed)
        Time.timeScale = 1;

        // Hide the slow down effect image
        slowDownEffectImage.enabled = false;
    }
    public void UseRewindSkill()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isMoving = false;
        float currentTime = Time.time;
        //transform.GetComponent<PlayerHealth>().currentHealth = health[0];
        StartCoroutine(Rewind(currentTime, 3f));
    }
    
    
    IEnumerator Rewind(float startTime, float duration)
    {
        while (Time.time - startTime < duration && positions.Count > 0)
        {
            while (positions.Count > 0)
            {
                Vector3 prevPosition = positions[positions.Count - 1];

                // Set the previous position and rotation of the player
                this.transform.position = prevPosition;
               
                positions.RemoveAt(positions.Count - 1);
                
            }
            yield return null;
        }
       // yield return null;
    }



    IEnumerator ResetTimeScaleCoroutine(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
    }


    private void OnDestroy()
    {
       
    }
}
