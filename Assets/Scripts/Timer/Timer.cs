using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CanvasGroup timerCamvasGroup;

    public void TimerVisibility(bool visible, float duration) 
    {
        timerCamvasGroup.DOFade(visible ? 1f : 0f, duration);
    }

    public void StartTimer(float duration, Action endTimerEvent) 
    {

    }
    public IEnumerator TimerStart(float duration) 
    {
        barImage.fillAmount = 1f;
        barImage.DOFillAmount(0f, duration);
        yield return new WaitForSeconds(duration);
    }
}
