using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialHandler
{
    [SerializeField] private TutorialUI tutorialUI;

    private TutorialUI[] tutorialUIs;

    public void Init(CardCombination[] cardCombinations) { }
}
