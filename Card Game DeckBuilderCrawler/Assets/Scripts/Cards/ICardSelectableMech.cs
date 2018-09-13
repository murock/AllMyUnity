using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardSelectableMech{
    void ApplySelectionAction(List<Transform> cardsSelected, List<Transform> cardsNotSelected);
}
