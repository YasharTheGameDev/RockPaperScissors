using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard
{
    public CardType CardType { get; }
    public void CardDeselect();
}
