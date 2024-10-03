using UnityEngine;

[System.Serializable]
public class GameScore 
{
    [SerializeField] private Score playerOneScore;
    [SerializeField] private Score playerTwoScore;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite playerOnePointSprite;
    [SerializeField] private Sprite playerTwoPointSprite;

    #region [- Behaviours -]
    public void Init(int points, Sprite playerOneIcon, Sprite playerTwoIcon)
    {
        playerOneScore.SetData(points, defaultSprite, playerOnePointSprite, playerOneIcon);
        playerTwoScore.SetData(points, defaultSprite, playerTwoPointSprite, playerTwoIcon);
    }
    public void AddScore(int playerNum)
    {
        if (playerNum == 1)
        {
            playerOneScore.AddScore();
        }
        else if (playerNum == 2)
        {
            playerTwoScore.AddScore();
        }
    }
    public int GetWinner()
    {
        if (playerOneScore.Win())
        {
            return 1;
        }
        else if (playerTwoScore.Win())
        {
            return 2;
        }
        return 0;
    }
    public void OnReset()
    {
        playerOneScore.OnReset();
        playerTwoScore.OnReset();
    } 
    #endregion
}
