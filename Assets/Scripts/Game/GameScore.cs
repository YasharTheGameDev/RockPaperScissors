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
    
    public void StartGame(int points) 
    {
        playerOneScore.SetData(points, defaultSprite, playerOnePointSprite);
        playerTwoScore.SetData(points, defaultSprite, playerTwoPointSprite);
    }
}
