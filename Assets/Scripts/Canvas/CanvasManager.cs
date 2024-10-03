using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    #region [- Audio -]
    public void SfxBtnPress()
    {
        AudioMaster.Instance.OnAudioMute();
    } 
    #endregion

    #region [- MiniMenu -]
    [SerializeField] private GameObject miniMenuCanvas;
    [SerializeField] private Image blackScreenImage;

    #region [- Tutorial -]
    [SerializeField] private GameObject tutorialCanvas;
    public void TutorialBtnPress(bool open)
    {
        StartCoroutine(MiniMenu(tutorialCanvas, open));
    }
    #endregion

    #region [- Home -]
    [SerializeField] private GameObject homeCanvas;
    public void HomeBtnPress(bool open)
    {
        StartCoroutine(MiniMenu(homeCanvas, open));
    }
    public void GoHomeBtnPress()
    {
        Time.timeScale = 1f;
        Action StartGameAction = () =>
        {
            //GameMaster.Instance.ResetGame();
            mainMenuCanvas.SetActive(true);
            playCanvas.SetActive(false);
            SceneManager.LoadScene(0);
        };
        StartCoroutine(canvasFade.Fade(.5f, .5f, StartGameAction));
    }
    #endregion

    private IEnumerator MiniMenu(GameObject canvas, bool open)
    {
        if (open)
        {
            miniMenuCanvas.SetActive(open);
            blackScreenImage.DOFade(.85f, .2f);
            yield return new WaitForSeconds(.2f);
            canvas.SetActive(open);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            canvas.SetActive(open);
            blackScreenImage.DOFade(0f, .2f);
            miniMenuCanvas.SetActive(open);
        }
    }
    #endregion

    #region [- Result -]
    [SerializeField] private CanvasResult canvasResult;
    public void ShowResult(int playerNum, Sprite playerOneIcon, Sprite playerTwoIcon) 
    {
        StartCoroutine(canvasResult.ShowWinner(playerNum, playerOneIcon, playerTwoIcon));
    }
    #endregion
}
