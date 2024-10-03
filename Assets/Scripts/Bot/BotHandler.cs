using UnityEngine;

public class BotHandler : MonoBehaviour
{
    [SerializeField] private BotNameCollection botNameCollection;

    private Card[] cards;
    private int playerNum;

    #region [- Behaviours -]
    public void AssignBot(int playerNum)
    {
        this.playerNum = playerNum;
        //GameMaster.Instance.EndSelectEvent += SendInput;
    }
    public void UnassignBot()
    {
        //GameMaster.Instance.EndSelectEvent -= SendInput;
    }
    public void SendInput()
    {
        GameMaster.Instance.GetInput(playerNum, cards[Random.Range(0, cards.Length)].CardType);
    }
    public void GetCards(Card[] cards)
    {
        this.cards = cards;
    } 
    #endregion
}
