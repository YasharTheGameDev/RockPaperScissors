using DG.Tweening;
using System;
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

    #region [- SimpleGameStart -]
    [SerializeField] private GameRule simpleGameRule;
    public void StartSimpleGame()
    {
        StartGame(simpleGameRule, GameType.PlayerVsBot);
    } 
    #endregion

    [SerializeField] private BotHandler botHandler;
    [SerializeField] private PlayerHandler playerHandler;

    private GameRule currentGameRule;
    private GameType currentGameType;

    public void StartGame(GameRule gameRule, GameType gameType) 
    {
        ResetGame();

        StartGameEvent?.Invoke();

        currentGameRule = gameRule;
        currentGameType = gameType;

        StartCoroutine(Intro());
    }

    private IEnumerator Intro() 
    {
        gameScore.Init(currentGameRule.ScoreToWin);

        yield return new WaitForSeconds(3);

        switch (currentGameType)
        {
            case GameType.PlayerVsBot:
                playerHandler.StartGame(1);
                botHandler.AssignBot(2);
                break;
            case GameType.PlayerVsPlayerLocal:
                break;
            case GameType.PlayerVsPlayerOnline:
                break;
        }

        StartCoroutine(DealCards());
    }

    public void ResetGame() 
    {
        ResetGameEvent?.Invoke();
    }

    #region [- Card Pos -]
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
    #endregion

    #region [- Cards -]
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
    private IEnumerator HideCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            StartCoroutine(card.FlipCard(true));
            card.transform.DOMove(cardParent.position, .5f);

            yield return new WaitForSeconds(.25f);
        }
        yield return null;
        foreach (Card card in cards) 
        {
            card.gameObject.SetActive(false);
        }
    }

    private void DeleteCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            Destroy(cards[i]);
        }
        cards = null;
    } 
    #endregion

    #region [- Score -]

    [SerializeField] private GameScore gameScore;

    private IEnumerator RevealCards() 
    {
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(1f);
        
    }

    #endregion

    #region [- Input -]
    public void GetInput(int playerNum, CardType cardType) 
    {
        if (playerNum == 1)
        {
            playerOneSelectedCardType = cardType;
            playerOneSelect = true;
        } 
        else if (playerNum == 2) 
        {
            playerTwoSelectedCardType = cardType;
            playerTwoSelect = true;
        }

        if (playerOneSelect && playerTwoSelect)
        {
        }
    }

    private bool playerOneSelect;
    private CardType playerOneSelectedCardType;
    private bool playerTwoSelect;
    private CardType playerTwoSelectedCardType;
    #endregion

    public Action ResetGameEvent;
    public Action StartGameEvent;
    public Action DealCardsEvent;
    public Action StartSelectEvent;
    public Action EndSelectEvent;
    public Action ShowCardsEvent;
    public Action ResultEvent;
}
