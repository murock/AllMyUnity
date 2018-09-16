using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for selectable cards that use the center panel
/// </summary>
public interface ICardSelectableMech{
    void ApplySelectionAction(List<Transform> cardsSelected, List<Transform> cardsNotSelected);
}
