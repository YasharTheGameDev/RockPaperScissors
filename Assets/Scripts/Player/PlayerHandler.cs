using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour, IPlayerCardSelect
{
    private Card[] cards;
    private ICard selectedCard;
    private int playerNum;

    public void GetCards(Card[] cards) 
    {
        this.cards = cards;
        foreach (Card card in this.cards) 
        {
            card.SetPlayer(this);
        }
        SelectAllow = true;
    }
    public void StartGame(int playerNum) 
    {
        this.playerNum = playerNum;
    }
    public void SelectCard(ICard card) 
    {
        if (selectedCard != null)
        {
            if (selectedCard == card)
            {
                return;
            }
            else
            {
                selectedCard.CardDeselect();
            }
        }

        selectedCard = card;
    }
    public void SendInput() 
    {
        CardType cardType = selectedCard != null ? selectedCard.CardType 
        : cards[Random.Range(0, cards.Length)].CardType;

        GameMaster.Instance.GetInput(playerNum, cardType);
        selectedCard = null;
    }

    public void AssignBot() 
    {
    }
    public void UnassignBot() 
    {
    }

    public bool SelectAllow { get; private set; }
}
