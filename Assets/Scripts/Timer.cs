using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time = 90.0f; // czas w sekundach
    [SerializeField] private float timeFactor = 1.0f; // współczynnik przyspieszenia czasu
    [SerializeField] private bool isTimerPaused = false;
    [SerializeField] private TextMeshProUGUI timeText;
    void Start()
    {
        
    }
    void Update()
    {
        if (!isTimerPaused)
        {
            time -= Time.deltaTime * timeFactor;
            if (time <= 0)
            {
                time = 0;
                isTimerPaused = true;
                Debug.Log("Time's up!");
            }
            
            // update text
            timeText.text = time.ToString("F2");
        }
    }
    public void addTime(float timeToAdd)
    {
        time += timeToAdd;
    }
    public void pauseTimer()
    {
        isTimerPaused = true;
    }
    public void resumeTimer()
    {
        isTimerPaused = false;
    }
}
