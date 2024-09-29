using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Timer timer;

    public void TimerVisibility(bool visible, float duration) 
    {
        timer.TimerVisibility(visible, duration);
    }
    public void StartTimer(float duration, Action endTimerEvent) 
    {
        timer.StartTimer(duration, endTimerEvent);
    }
}
