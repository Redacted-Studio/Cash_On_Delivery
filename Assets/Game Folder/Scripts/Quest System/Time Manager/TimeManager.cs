using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Static Method

    // Call Every Changes
    public static Action onMinuteChange;
    public static Action onHourChange;

    // Get Starting Time
    public static Action onGetStartingTimeMinute;
    public static Action onGetStartingTimeHour;

    public GameObject Sun;

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    // Time Manager
    [Tooltip("How Many Second To Minute In Game")][Range(1, 60)][SerializeField] private int minuteToRT = 5;
    [SerializeField][Range(0, 59)] private int startingMinute;
    [SerializeField][Range(0, 23)] private int startingHour;

    private float timer;


    void Start()
    {
        Minute = startingMinute;
        Hour = startingHour;
        timer = minuteToRT;
        onGetStartingTimeMinute?.Invoke();
        onGetStartingTimeHour?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Minute++;
            onMinuteChange?.Invoke();
            if (Minute >= 60)
            {
                
                Hour++;
                onHourChange?.Invoke();
                Minute = 0;
            }

            timer = minuteToRT;
        }
    }

    public int getMinute()
    {
        return Minute;
    }
}
