using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card combination", menuName = "Card/Card Combination", order = 2)]
public class CardCombination : ScriptableObject
{
    [SerializeField] private CardType winnerCardType;
    [SerializeField] private CardType loserCardType;

    public CardCombinationResult Result(CardType playerOneCardType, CardType playerTwoCardType) 
    {
        bool winnerCard = false;
        bool loserCard = false;

        #region [- Player One -]
        if (playerOneCardType == winnerCardType)
        {
            winnerCard = true;
        }
        else if (playerOneCardType == loserCardType)
        {
            winnerCard = true;
        }
        #endregion

        #region [- Player Two -]
        if (playerTwoCardType == winnerCardType)
        {
            winnerCard = true;
        }
        else if (playerTwoCardType == loserCardType)
        {
            winnerCard = true;
        } 
        #endregion

        if (winnerCard && loserCard) 
        {
            if (winnerCardType == playerOneCardType) 
            {
                return CardCombinationResult.WinnerP1;
            }
            else if (winnerCardType == playerTwoCardType) 
            {
                return CardCombinationResult.WinnerP2;
            }
        }

        return CardCombinationResult.Null;
    }
}
