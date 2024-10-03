using UnityEngine;

[System.Serializable]
public class TutorialHandler: MonoBehaviour
{
    [SerializeField] private Transform tutorialParent;
    [SerializeField] private TutorialUI tutorialUI;

    private TutorialUI[] tutorialUIs;

    #region [- Behaviours -]
    public void Init(CardCombinationList cardCombinationList)
    {
        CardCombination[] cardCombinations = cardCombinationList.CardCombinations;
        tutorialUIs = new TutorialUI[cardCombinations.Length];

        foreach (var combination in cardCombinations)
        {
            TutorialUI ui = Instantiate(tutorialUI, tutorialParent);
            ui.Init(combination.WinnerCardType.CardSmallSprite, combination.LoserCardType.CardSmallSprite);
        }
    } 
    #endregion
}
