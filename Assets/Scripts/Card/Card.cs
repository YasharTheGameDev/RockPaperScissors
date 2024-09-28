using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
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

    public void CardSelect()
    {
        
    }
    public void CardDeselect() 
    {

    }
}
