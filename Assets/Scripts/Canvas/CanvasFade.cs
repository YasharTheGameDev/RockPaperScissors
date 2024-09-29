using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CanvasFade 
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image fadeImg;

    public IEnumerator Fade(bool fadeIn, float durtaion) 
    {
        Color color = fadeImg.color;
        color.a = fadeIn ? 0f : 1f;
        fadeImg.color = color;
        canvas.SetActive(true);
        fadeImg.DOFade(fadeIn ? 1f : 0f, durtaion);
        yield return new WaitForSeconds(durtaion);
        canvas.SetActive(false);
    }

    public IEnumerator Fade(bool fadeIn, float durtaion, float startDelay)
    {
        canvas.SetActive(true);
        Color color = fadeImg.color;
        color.a = fadeIn ? 0f : 1f;
        fadeImg.color = color;
        yield return new WaitForSeconds(startDelay);
        fadeImg.DOFade(fadeIn ? 1f : 0f, durtaion);
        yield return new WaitForSeconds(durtaion);
        canvas.SetActive(false);
    }

    public IEnumerator Fade(float durtaion, float stayDuration, Action action) 
    {
        Color color = fadeImg.color;
        color.a = 0f;
        fadeImg.color = color;
        canvas.SetActive(true);
        fadeImg.DOFade(1f, durtaion);
        yield return new WaitForSeconds(durtaion);
        action?.Invoke();
        yield return new WaitForSeconds(stayDuration);
        fadeImg.DOFade(0f, durtaion);
        yield return new WaitForSeconds(durtaion);
        canvas.SetActive(false);
    }
}
