using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    public Action ResetGameEvent;
    public Action StartGameEvent;
    public Action StartSelectEvent;
    public Action EndSelectEvent;
    public Action<int> ResultEvent;

    #region [- Unity -]
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tutorialHandler.Init(cardCombinationList);
    } 
    #endregion

    #region [- SimpleGameStart -]
    [SerializeField] private GameRule simpleGameRule;
    public void StartSimpleGame()
    {
        StartGame(simpleGameRule, GameType.PlayerVsBot);
    }
    #endregion

    #region [- StartGame -]
    [SerializeField] private BotHandler botHandler;
    [SerializeField] private PlayerHandler playerHandler;

    private GameRule currentGameRule;
    private GameType currentGameType;

    #region [- Behaviours -]
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
        switch (currentGameType)
        {
            case GameType.PlayerVsBot:
                gameScore.Init(currentGameRule.ScoreToWin, humanPlayerSprite, botPlayerSprite);
                break;
            case GameType.PlayerVsPlayerLocal:
                gameScore.Init(currentGameRule.ScoreToWin, humanPlayerSprite, humanPlayerSprite);
                break;
            case GameType.PlayerVsPlayerOnline:
                gameScore.Init(currentGameRule.ScoreToWin, humanPlayerSprite, humanPlayerSprite);
                break;
        }

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
    private IEnumerator StartNextRound()
    {
        playerOneSelect = false;
        playerTwoSelect = false;

        yield return new WaitForSeconds(1f);

        StartCoroutine(playerOneResultCard.FlipCard(false));
        playerOneResultCard.transform.DOMove(cardParent.position, 1f);

        StartCoroutine(playerTwoResultCard.FlipCard(false));
        playerTwoResultCard.transform.DOMove(opponentCardParent.position, 1f);

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(DealCards());
    }
    public void ResetGame()
    {
        ResetGameEvent?.Invoke();
        DeleteCards();
        DeleteCardPos();
        gameScore.OnReset();

        playerOneSelect = false;
        playerTwoSelect = false;
    }  
    #endregion
    #endregion

    #region [- Card Pos -]

    [SerializeField] private Transform cardsPositionParent;
    [SerializeField] private Transform cardPosition;

    private Transform[] cardPositions;

    #region [- Behaviours -]
    private void CreateCardPos(int cardsNo)
    {
        cardPositions = new Transform[cardsNo];
        for (int i = 0; i < cardsNo; i++)
        {
            cardPositions[i] = Instantiate(cardPosition, cardsPositionParent);
        }
    }
    private void DeleteCardPos()
    {
        if (cardPositions != null && cardPositions.Length > 0)
        {
            for (int i = 0; i < cardPositions.Length; i++)
            {
                Destroy(cardPositions[i]);
            }
            cardPositions = null;
        }
    } 
    #endregion

    #endregion

    #region [- Cards -]
    [SerializeField] private CardCollection cardCollection;
    [SerializeField] private Card card;
    [SerializeField] private Transform cardParent;
    [SerializeField] private Transform opponentCardParent;

    [SerializeField] private Card playerOneResultCard;
    [SerializeField] private Transform playerOneRevealPos;
    [SerializeField] private Card playerTwoResultCard;
    [SerializeField] private Transform playerTwoRevealPos;

    [SerializeField] private AudioFile showCardAudio;

    private Card[] cards;

    #region [- Behaviours -]
    public IEnumerator DealCards()
    {
        if (cards != null && cards.Length > 0)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].ResetInput();
                cards[i].gameObject.SetActive(true);
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

        yield return new WaitForSeconds(1.5f);

        playerHandler.GetCards(cards);
        if (currentGameType == GameType.PlayerVsBot)
        {
            botHandler.GetCards(cards);
        }

        timerManager.TimerVisibility(true, .5f);

        yield return new WaitForSeconds(.5f);

        Action endTimerEvent = () =>
        {
            StartCoroutine(HideCards());
        };
        timerManager.StartTimer(currentGameRule.SelectDuration, endTimerEvent);
        StartSelectEvent?.Invoke();
    }
    private IEnumerator ShowCard(Card card, int index)
    {
        AudioMaster.Instance.PlayAudio(showCardAudio);
        card.transform.DOMove(cardPositions[index].position, .5f);
        card.transform.DORotate(new Vector3(0, 0, 180), .25f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(card.FlipCard(true));
    }
    private IEnumerator HideCards()
    {
        EndSelectEvent?.Invoke();

        timerManager.TimerVisibility(false, .5f);

        for (int i = 0; i < cards.Length; i++)
        {
            StartCoroutine(cards[i].FlipCard(false));
            cards[i].transform.DOMove(cardParent.position, .5f);

            yield return new WaitForSeconds(.25f);
        }

        yield return new WaitForSeconds(.5f);

        foreach (Card card in cards)
        {
            card.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1f);

        playerHandler.SendInput();
        botHandler.SendInput();
    }
    private void DeleteCards()
    {
        if (cards != null && cards.Length > 0)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                Destroy(cards[i]);
            }
            cards = null;
        }
    }  
    #endregion
    #endregion

    #region [- Score -]

    [SerializeField] private GameScore gameScore;
    [SerializeField] private Sprite humanPlayerSprite;
    [SerializeField] private Sprite botPlayerSprite;
    [SerializeField] private Image cardRevealImage;

    #region [- Behaviours -]
    private IEnumerator RevealCards()
    {
        cardRevealImage.DOFade(1f, .25f);

        AudioMaster.Instance.PlayAudio(showCardAudio);

        playerOneResultCard.SetData(cardCollection.GetCard(playerOneSelectedCardType));
        playerOneResultCard.transform.position = cardParent.position;
        playerOneResultCard.transform.DOMove(playerOneRevealPos.position, 1f);

        playerTwoResultCard.SetData(cardCollection.GetCard(playerTwoSelectedCardType));
        playerTwoResultCard.transform.position = opponentCardParent.position;
        playerTwoResultCard.transform.DOMove(playerTwoRevealPos.position, 1f);

        yield return new WaitForSeconds(2f);

        StartCoroutine(playerOneResultCard.FlipCard(true));
        StartCoroutine(playerTwoResultCard.FlipCard(true));

        yield return new WaitForSeconds(1f);

        Debug.Log("CheckResult()");
        StartCoroutine(CheckResult()); 
    }
    #endregion

    #endregion

    #region [- Tutorial -]
    [SerializeField] private TutorialHandler tutorialHandler; 
    #endregion

    #region [- Timer -]
    [SerializeField] private TimerManager timerManager;
    #endregion

    #region [- Input -]
    private bool playerOneSelect;
    private CardType playerOneSelectedCardType;
    private bool playerTwoSelect;
    private CardType playerTwoSelectedCardType;

    #region [- Behaviours -]
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
            Debug.Log("RevealCards()");
            StartCoroutine(RevealCards());
        }
    } 
    #endregion
    #endregion

    #region [- Combination -]
    [SerializeField] private CardCombinationList cardCombinationList;
    [SerializeField] private AudioFile scoreAudio;
    [SerializeField] private AudioFile drawAudio;

    [SerializeField] private Image resultHalo;
    [SerializeField] private Color resultDrawColor;
    [SerializeField] private Color resultPlayerOneColor;
    [SerializeField] private Color resultPlayerTwoColor;

    #region [- Behaviours -]
    private IEnumerator CheckResult()
    {
        Debug.Log("CheckResult");
        CardCombinationResult cardCombinationResult =
            cardCombinationList.Result(playerOneSelectedCardType, playerTwoSelectedCardType);

        switch (cardCombinationResult)
        {
            case CardCombinationResult.Draw:
                AudioMaster.Instance.PlayAudio(drawAudio);
                break;
            case CardCombinationResult.WinnerP1:
                gameScore.AddScore(1);
                AudioMaster.Instance.PlayAudio(scoreAudio);
                break;
            case CardCombinationResult.WinnerP2:
                gameScore.AddScore(2);
                AudioMaster.Instance.PlayAudio(scoreAudio);
                break;
        }

        StartCoroutine(ResultColor(cardCombinationResult));

        yield return new WaitForSeconds(1f);

        cardRevealImage.DOFade(0f, .25f);

        yield return new WaitForSeconds(.5f);

        int winnerNum = gameScore.GetWinner();
        Debug.Log("winnerNum: " + winnerNum);
        if (winnerNum == 0)
        {
            StartCoroutine(StartNextRound());
        }
        else
        {
            Sprite playerOneIcon = botPlayerSprite;
            Sprite playerTwoIcon = botPlayerSprite;
            switch (currentGameType) 
            {
                case GameType.PlayerVsBot:
                    playerOneIcon = humanPlayerSprite;
                    playerTwoIcon = botPlayerSprite;
                    break;

            }
            CanvasManager.Instance.ShowResult(winnerNum, playerOneIcon, playerTwoIcon);
        }
    }
    private IEnumerator ResultColor(CardCombinationResult cardCombinationResult)
    {
        switch (cardCombinationResult)
        {
            case CardCombinationResult.Draw:
                resultHalo.color = resultDrawColor;
                break;
            case CardCombinationResult.WinnerP1:
                resultHalo.color = resultPlayerOneColor;
                break;
            case CardCombinationResult.WinnerP2:
                resultHalo.color = resultPlayerTwoColor;
                break;
        }

        resultHalo.DOFade(.75f, .25f);

        yield return new WaitForSeconds(1f);

        resultHalo.DOFade(0f, .25f);
    } 
    #endregion
    #endregion
}
