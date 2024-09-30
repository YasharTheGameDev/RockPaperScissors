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
        barImage.fillAmount = visible ? 1f : 0f;
        timerCamvasGroup.DOFade(visible ? 1f : 0f, duration);
    }

    public IEnumerator StartTimer(float duration, Action endTimerEvent) 
    {
        barImage.fillAmount = 1f;
        barImage.DOFillAmount(0f, duration).SetEase(Ease.Linear);
        yield return new WaitForSeconds(duration);
        endTimerEvent?.Invoke();
    }
}
