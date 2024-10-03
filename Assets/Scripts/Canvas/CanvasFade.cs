using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CanvasFade 
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private CanvasGroup fadeCanvasGroup;

    #region [- Behaviours -]
    public IEnumerator Fade(bool fadeIn, float durtaion)
    {
        fadeCanvasGroup.alpha = fadeIn ? 0f : 1f;
        canvas.SetActive(true);
        fadeCanvasGroup.DOFade(fadeIn ? 1f : 0f, durtaion);
        yield return new WaitForSeconds(durtaion);
        canvas.SetActive(false);
    }
    public IEnumerator Fade(bool fadeIn, float durtaion, float startDelay)
    {
        canvas.SetActive(true);
        fadeCanvasGroup.alpha= fadeIn ? 0f : 1f;
        yield return new WaitForSeconds(startDelay);
        fadeCanvasGroup.DOFade(fadeIn ? 1f : 0f, durtaion);
        yield return new WaitForSeconds(durtaion);
        canvas.SetActive(false);
    }
    public IEnumerator Fade(float durtaion, float stayDuration, Action action)
    {
        fadeCanvasGroup.alpha = 0;
        canvas.SetActive(true);
        fadeCanvasGroup.DOFade(1f, durtaion);
        yield return new WaitForSeconds(durtaion);
        action?.Invoke();
        yield return new WaitForSeconds(stayDuration);
        fadeCanvasGroup.DOFade(0f, durtaion);
        yield return new WaitForSeconds(durtaion);
        canvas.SetActive(false);
    } 
    #endregion
}
