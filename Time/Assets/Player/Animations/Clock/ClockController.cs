using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClockController : MonoBehaviour
{
    public Transform hoursHand;
    public Transform minutesHand;
    public Transform secondsHand;
    public float duration = 3f; 

    void Update()
    {
        //// Get the current time
        //float hours = System.DateTime.Now.Hour % 12;
        //float minutes = System.DateTime.Now.Minute;
        //float seconds = System.DateTime.Now.Second;

        //// Set the rotation of the hour hand
        //float hoursAngle = hours * 30f + minutes / 2f;
        //hoursHand.rotation = Quaternion.Euler(0f, 0f, -hoursAngle);

        //// Set the rotation of the minute hand
        //float minutesAngle = minutes * 6f;
        //minutesHand.rotation = Quaternion.Euler(0f, 0f, -minutesAngle);

        //// Set the rotation of the second hand
        //float secondsAngle = seconds * 6f;
        //secondsHand.rotation = Quaternion.Euler(0f, 0f, -secondsAngle);
        //for (int i = 0; i < duration; i++)
        //{

        //}
        
    }
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
}

