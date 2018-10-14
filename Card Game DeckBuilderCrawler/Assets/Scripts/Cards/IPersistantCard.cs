using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistantCard {

    void SetTurnsToPersist(int turnsToPersist);
    void ApplyPersistAction();
}
