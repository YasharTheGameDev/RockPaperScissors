using UnityEngine;

public class PlayerHandler : MonoBehaviour, IPlayerCardSelect
{
    public bool SelectAllow { get; private set; }

    private Card[] cards;
    private ICard selectedCard;
    private int playerNum;

    #region [- Behaviours -]
    public void GetCards(Card[] cards)
    {
        this.cards = cards;
        foreach (Card card in this.cards)
        {
            card.SetPlayer(this);
        }
    }
    public void StartGame(int playerNum)
    {
        this.playerNum = playerNum;
        GameMaster.Instance.StartSelectEvent += () => { SelectAllow = true; };
        GameMaster.Instance.EndSelectEvent += () => { SelectAllow = false; };
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
    #endregion
}
