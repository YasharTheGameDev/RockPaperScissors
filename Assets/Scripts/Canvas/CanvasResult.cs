using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class CanvasResult
{
    [SerializeField] private GameObject resultCanvas;

    [SerializeField] private Image playerOneIconImage;
    [SerializeField] private Image playerTwoIconImage;

    [SerializeField] private Image backGround;
    [SerializeField] private Color playeOneBackgroundColor;
    [SerializeField] private Color playeTwoBackgroundColor;

    [SerializeField] private Image crownImage;

    #region [- Behaviours -]
    public IEnumerator ShowWinner(int winnerNum, Sprite playerOneIcon, Sprite playerTwoIcon)
    {
        CanvasVisibility(true);

        playerOneIconImage.sprite = playerOneIcon;
        playerTwoIconImage.sprite = playerTwoIcon;

        if (winnerNum == 1)
        {
            backGround.color = playeOneBackgroundColor;
            crownImage.transform.position = playerOneIconImage.transform.position;
        }
        else if (winnerNum == 2)
        {
            backGround.color = playeTwoBackgroundColor;
            crownImage.transform.position = playerTwoIconImage.transform.position;
        }

        backGround.DOFade(.75f, .5f);

        crownImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(.5f);

        crownImage.gameObject.SetActive(true);
        crownImage.DOFade(1f,.5f);
        crownImage.transform.DOMove(crownImage.transform.position + new Vector3(0,100,0), .5f);
    }
    public void CanvasVisibility(bool visible)
    {
        resultCanvas.SetActive(visible);

        Color color = backGround.color;
        color.a = 0;
        backGround.color = color;
    } 
    #endregion
}
