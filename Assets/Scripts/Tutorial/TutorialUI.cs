using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private Image firstCardImage;
    [SerializeField] private Image secondCardImage;
    [SerializeField] private Image resultCardImage;

    #region [- Behaviours -]
    public void Init(Sprite firstCardSprite, Sprite secondCardSprite)
    {
        firstCardImage.sprite = firstCardSprite;
        secondCardImage.sprite = secondCardSprite;
        resultCardImage.sprite = firstCardSprite;
    } 
    #endregion
}
