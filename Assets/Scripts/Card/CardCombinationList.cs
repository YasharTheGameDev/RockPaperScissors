using UnityEngine;

[CreateAssetMenu(fileName = "New Card combination list", menuName = "Card/Card Combination List", order = 3)]
public class CardCombinationList : ScriptableObject
{
    [field: SerializeField] public CardCombination[] CardCombinations { get; private set; }

    public CardCombinationResult Result(CardType playerOneCardType, CardType playerTwoCardType)
    {
        if (playerOneCardType == playerTwoCardType)
        {
            return CardCombinationResult.Draw;
        }

        foreach (CardCombination cardCombination in CardCombinations)
        {
            CardCombinationResult result = cardCombination.Result(playerOneCardType, playerTwoCardType);
            Debug.Log(result);
            if (result != CardCombinationResult.Null)
            {
                return result;
            }
        }

        return CardCombinationResult.Null;
    }
}
