using UnityEngine;

[System.Serializable]
public class GameCardRule
{
    [field: SerializeField] public CardType CardType { get; private set; }
    [field: SerializeField, Range(1,5)] public int Count { get; private set; }
}
