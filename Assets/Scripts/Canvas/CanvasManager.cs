using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(canvasFade.Fade(false, .5f, 2f));
    }

    [SerializeField] private CanvasFade canvasFade;

    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject playCanvas;

    public void StartGameBtnPress() 
    {
        Action StartGameAction = () => 
        {
            GameMaster.Instance.StartSimpleGame();
            mainMenuCanvas.SetActive(false);
            playCanvas.SetActive(true);
        };
        StartCoroutine(canvasFade.Fade(.5f, 1f, StartGameAction));
    }

    public void SfxBtnPress() { }
    public void InfoBtnPress(bool open) { }
    public void HomeBtnPress() { }
}
