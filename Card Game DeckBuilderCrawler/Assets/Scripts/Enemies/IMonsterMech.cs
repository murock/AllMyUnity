using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterMech {
    string GetToolTip();
    void OnSpawn();
    void OnDeath();
}
