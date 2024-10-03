using UnityEngine;

[CreateAssetMenu(fileName = "New Card combination", menuName = "Card/Card Combination", order = 2)]
public class CardCombination : ScriptableObject
{
    [field: SerializeField] public CardAsset WinnerCardType { get; private set; }
    [field: SerializeField] public CardAsset LoserCardType { get; private set; }

    #region [- Behaviours -]
    public CardCombinationResult Result(CardType playerOneCardType, CardType playerTwoCardType)
    {
        bool winnerCard = false;
        bool loserCard = false;

        #region [- Player One -]
        if (playerOneCardType == WinnerCardType.CardType)
        {
            winnerCard = true;
        }
        else if (playerOneCardType == LoserCardType.CardType)
        {
            loserCard = true;
        }
        #endregion

        #region [- Player Two -]
        if (playerTwoCardType == WinnerCardType.CardType)
        {
            winnerCard = true;
        }
        else if (playerTwoCardType == LoserCardType.CardType)
        {
            loserCard = true;
        }
        #endregion

        if (winnerCard && loserCard)
        {
            Debug.Log("TRUE");
            if (WinnerCardType.CardType == playerOneCardType)
            {
                return CardCombinationResult.WinnerP1;
            }
            else if (WinnerCardType.CardType == playerTwoCardType)
            {
                return CardCombinationResult.WinnerP2;
            }
        }

        return CardCombinationResult.Null;
    }
    #endregion
}
