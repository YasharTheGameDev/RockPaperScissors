using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game rule", menuName = "Game/Game Rule", order = 1)]
public class GameRule : ScriptableObject
{
    [field: SerializeField] public int ScoreToWin { get; private set; }
    [field: SerializeField] public float SelectDuration { get; private set; }
    [field: SerializeField] public GameCardRule[] Cards { get; private set; }
}
