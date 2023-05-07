using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRiftAim : MonoBehaviour
{
    public GameObject timeRiftPrefab;

    private bool isAiming = false;
    private Vector3 startPoint;
    private LineRenderer lineRenderer;

    public GameObject player;

    void Start()
    {
        // Attach a LineRenderer to the game object to draw a line while aiming
        lineRenderer = player.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.material.color = Color.yellow;
    }

    void Update()
    {
        if (isAiming)
        {
            // Update the line renderer to draw a line from the player to the current mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, mousePos);
        }
    }

    public void OnPointerDown()
    {
        isAiming = true;
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPoint.z = 0;
        lineRenderer.enabled = true;
    }

    public void OnPointerUp()
    {
        isAiming = false;
        lineRenderer.enabled = false;
        Vector3 endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPoint.z = 0;
        Instantiate(timeRiftPrefab, endPoint, Quaternion.identity);
    }
}
