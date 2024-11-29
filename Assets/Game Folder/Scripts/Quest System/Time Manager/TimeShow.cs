using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeShow : MonoBehaviour
{
    public TextMeshProUGUI TextMeshProUGUI;
    private void Start()
    {
        TimeManager.onGetStartingTimeMinute += UpdateTime;
        TimeManager.onGetStartingTimeHour += UpdateTime;
        UpdateTime();
    }

    private void Awake()
    {
        TimeManager.onMinuteChange += UpdateTime;
        TimeManager.onHourChange += UpdateTime;
    }
    private void OnDisable()
    {
        TimeManager.onMinuteChange -= UpdateTime;
        TimeManager.onHourChange -= UpdateTime;
    }

    private void UpdateTime()
    {
        TextMeshProUGUI.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }
}
