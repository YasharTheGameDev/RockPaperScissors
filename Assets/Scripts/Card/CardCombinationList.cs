using UnityEngine;

[CreateAssetMenu(fileName = "New Card combination list", menuName = "Card/Card Combination List", order = 3)]
public class CardCombinationList : ScriptableObject
{
    [SerializeField] private CardCombination[] cardCombinations;
}
