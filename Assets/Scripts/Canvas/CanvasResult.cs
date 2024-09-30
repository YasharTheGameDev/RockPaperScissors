using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResult
{
    [SerializeField] private Image playerOneIcon;
    [SerializeField] private Image playerTwoIcon;

    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite botSprite;

    [SerializeField] private Image backGround;
    [SerializeField] private Color playeOneBackgroundColor;
    [SerializeField] private Color playeTwoBackgroundColor;

    [SerializeField] private Image crownImage;

    public IEnumerator ShowWinner(int winnerNum, GameType gameType) 
    {
        switch (gameType) 
        {
            case GameType.PlayerVsBot:
                playerOneIcon.sprite = playerSprite;
                playerTwoIcon.sprite = botSprite;
                break;
            case GameType.PlayerVsPlayerLocal:
            case GameType.PlayerVsPlayerOnline:
                playerOneIcon.sprite = playerSprite;
                playerTwoIcon.sprite = playerSprite;
                break;
        }

        if (winnerNum == 1)
        {
            backGround.color = playeOneBackgroundColor;
        } else if (winnerNum == 2) 
        {
            backGround.color = playeTwoBackgroundColor;
        }

        crownImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

    }
}
