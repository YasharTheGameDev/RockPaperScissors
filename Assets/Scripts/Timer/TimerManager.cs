using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Timer timer;

    #region [- Behaviours -]
    public void TimerVisibility(bool visible, float duration)
    {
        timer.TimerVisibility(visible, duration);
    }
    public void StartTimer(float duration, Action endTimerEvent)
    {
        StartCoroutine(timer.StartTimer(duration, endTimerEvent));
    } 
    #endregion
}
