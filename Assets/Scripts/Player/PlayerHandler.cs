using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    private Card[] cards;
    private ICard selectedCard;
    private int playerNum;

    public void GiveCards(Card[] cards) 
    {
        this.cards = cards;
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
        GameMaster.Instance.GetInput(playerNum, selectedCard.CardType);
    }
}
