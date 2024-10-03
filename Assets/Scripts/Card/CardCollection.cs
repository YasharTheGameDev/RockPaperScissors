using UnityEngine;

[CreateAssetMenu(fileName = "New Card collection", menuName = "Card/Card Collection", order = 2)]
public class CardCollection : ScriptableObject
{
    [SerializeField] private CardAsset[] Cards;

    #region [- Behaviours -]
    public CardAsset GetCard(CardType cardType)
    {
        foreach (var card in Cards)
        {
            if (card.CardType == cardType)
            {
                return card;
            }
        }
        return null;
    } 
    #endregion
}
