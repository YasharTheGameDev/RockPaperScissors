using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

public class Card : MonoBehaviour, ICard,IPointerClickHandler
{
    public CardType CardType { get; private set; }
    [SerializeField] private Image cardMiddleImage;
    [SerializeField] private Image cardIconImage;

    [SerializeField] private GameObject cardHalo;
    [SerializeField] private GameObject cardBehind;
    [SerializeField] private GameObject cardFront;

    [SerializeField] private AudioFile flipAudio;
    [SerializeField] private AudioFile selectAudio;

    private IPlayerCardSelect playerCardSelect;

    public void SetData(CardAsset cardAsset)
    {
        cardHalo.SetActive(false);
        cardBehind.SetActive(true);
        cardFront.SetActive(false);

        CardType = cardAsset.CardType;
        cardMiddleImage.color = cardAsset.CardColor;
        cardIconImage.sprite = cardAsset.CardIconSprite;
    }
    public void ResetInput()
    {
        cardHalo.SetActive(false);
        cardBehind.SetActive(true);
        cardFront.SetActive(false);
    }
    public void SetPlayer(IPlayerCardSelect playerCardSelect) 
    { 
        this.playerCardSelect = playerCardSelect;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        CardSelect();
    }

    public IEnumerator FlipCard(bool front) 
    {
        cardFront.SetActive(!front);
        cardBehind.SetActive(front);

        transform.DORotate(new Vector3(0f, 90f, 0f), .1f);

        AudioMaster.Instance.PlayAudio(flipAudio);

        yield return new WaitForSeconds(.1f);

        cardFront.SetActive(front);
        cardBehind.SetActive(!front);

        transform.DORotate(new Vector3(0f, 0f, 0f), .1f);
    }

    public void CardSelect()
    {
        if (playerCardSelect != null && playerCardSelect.SelectAllow) 
        {
            AudioMaster.Instance.PlayAudio(selectAudio);
            cardHalo.SetActive(true);
            playerCardSelect.SelectCard(this);
        }
    }
    public void CardDeselect() 
    {
        cardHalo.SetActive(false);
    }
}
