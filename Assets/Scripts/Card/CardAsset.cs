using UnityEngine;

[CreateAssetMenu(fileName = "New Card asset", menuName = "Card/Card Asset", order = 1)]
public class CardAsset : ScriptableObject
{
    [field: SerializeField] public CardType CardType { get; private set; }
    [field: SerializeField] public Sprite CardIconSprite { get; private set; }
    [field: SerializeField] public Color CardColor { get; private set; }
    [field: SerializeField] public Sprite CardSmallSprite { get; private set; }
}
