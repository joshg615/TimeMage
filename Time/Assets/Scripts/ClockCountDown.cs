using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockCountDown : MonoBehaviour
{
    // Start is called before the first frame update
    public float totalTime = 60f; // total time for the countdown timer in seconds
    private float timeRemaining; // time remaining for the countdown timer in seconds
    public TMP_Text timerText; // reference to the UI text element to display the timer

    void Start()
    {
        timeRemaining = totalTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        // check if time is up
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            // add code to handle when the timer reaches zero
        }

        UpdateUI();
    }

    public void AddTime(float duration)
    {
        timeRemaining += duration;
    }

    void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        string timerString = string.Format("Count Down: {0:00}:{1:00}", minutes, seconds);
        timerText.text = timerString;
    }
}
