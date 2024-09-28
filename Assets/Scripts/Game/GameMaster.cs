using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    private void Awake()
    {
        Instance = this;
    }

    private GameRule currentGameRule;

    [SerializeField] private GameRule testGameRule;
    IEnumerator Start() 
    {
        yield return new WaitForSeconds(2);
        StartGame(testGameRule);
    }

    public void StartGame(GameRule gameRule) 
    {
        ResetGame();

        currentGameRule = gameRule;
        StartCoroutine(DealCards());
    }

    public void ResetGame() 
    {

    }

    [SerializeField] private Transform cardsPositionParent;
    [SerializeField] private Transform cardPosition;
    [SerializeField] private Transform cardParent;
    private Transform[] cardPositions;
    private void CreateCardPos(int cardsNo) 
    {
        if (cardPositions != null && cardPositions.Length > 0) 
        {
            DeleteCardPos();
        }

        cardPositions = new Transform[cardsNo];
        for (int i = 0; i < cardsNo; i++) 
        {
            cardPositions[i] = Instantiate(cardPosition, cardsPositionParent);
        }
    }
    private void DeleteCardPos() 
    {
        for (int i = 0; i < cardPositions.Length; i++)
        {
            Destroy(cardPositions[i]);
        }
        cardPositions = null;
    }

    [SerializeField] private CardCollection cardCollection;
    [SerializeField] private Card card;
    private Card[] cards;
    public IEnumerator DealCards() 
    {
        if (cards != null && cards.Length > 0)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].ResetInput();
            }
        }
        else 
        {
            List<CardAsset> cardAssets = new List<CardAsset>();
            for (int i = 0; i < currentGameRule.Cards.Length; i++) 
            {
                CardAsset cardAsset = cardCollection.GetCard(currentGameRule.Cards[i].CardType);
                for (int j = 0; j < currentGameRule.Cards[i].Count; j++) 
                {
                    cardAssets.Add(cardAsset);
                }
            }

            yield return null;

            CreateCardPos(cardAssets.Count);

            yield return null;

            cards = new Card[cardAssets.Count];
            for (int i = 0; i < cardAssets.Count; i++) 
            {
                Card newCard = Instantiate(card, cardParent);
                cards[i] = newCard;
                newCard.SetData(cardAssets[i]);
            }
        }

        yield return null;

        for (int i = 0; i < cards.Length; i++)
        {
            StartCoroutine(ShowCard(cards[i], i));
            yield return new WaitForSeconds(.25f);
        }
    }

    private IEnumerator ShowCard(Card card, int index)
    {
        card.transform.DOMove(cardPositions[index].position, .5f);
        card.transform.DORotate(new Vector3(0, 0, 180), .25f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(card.FlipCard(true));
    }
    private IEnumerator HideCard(Card card) 
    {
        yield return null;
    }

    private void DeleteCards() 
    {
        for (int i = 0; i < cards.Length; i++)
        {
            Destroy(cards[i]);
        }
        cards = null;
    }

    #region [- Score -]
    [SerializeField] private GameScore gameScore; 
    
    #endregion

}
