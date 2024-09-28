using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

public class Card : MonoBehaviour, IPointerClickHandler
{
    private CardType cardType;
    [SerializeField] private Image cardMiddleImage;
    [SerializeField] private Image cardIconImage;

    [SerializeField] private GameObject cardHalo;
    [SerializeField] private GameObject cardBehind;
    [SerializeField] private GameObject cardFront;

    public void SetData(CardAsset cardAsset)
    {
        cardHalo.SetActive(false);
        cardBehind.SetActive(true);
        cardFront.SetActive(false);

        cardType = cardAsset.CardType;
        cardMiddleImage.color = cardAsset.CardColor;
        cardIconImage.sprite = cardAsset.CardIconSprite;
    }
    public void ResetInput()
    {

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

        yield return new WaitForSeconds(.1f);

        cardFront.SetActive(front);
        cardBehind.SetActive(!front);

        transform.DORotate(new Vector3(0f, 0f, 0f), .1f);
    }

    public void CardSelect()
    {
        cardHalo.SetActive(true);
    }
    public void CardDeselect() 
    {
        cardHalo.SetActive(false);
    }
}
