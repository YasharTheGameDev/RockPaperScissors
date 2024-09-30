using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameScore 
{
    [SerializeField] private Score playerOneScore;
    [SerializeField] private Score playerTwoScore;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite playerOnePointSprite;
    [SerializeField] private Sprite playerTwoPointSprite;
    
    public void Init(int points) 
    {
        playerOneScore.SetData(points, defaultSprite, playerOnePointSprite);
        playerTwoScore.SetData(points, defaultSprite, playerTwoPointSprite);
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
}
