using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDrawMech :  IMonsterMech {

    string IMonsterMech.GetToolTip()
    {
        return "Cannot Draw Cards";
    }

    void IMonsterMech.OnDeath()
    {
        // Needs to be a check here to see if there are any other monsters than block draw alive
        foreach (MonsterInteraction monsterSpawner in TurnManager.Instance.monsterSpawners)
        {
            if (monsterSpawner)
            {

            }
        }
        
        CardDrawMech.IsDrawAllowed = true;
    }

    void IMonsterMech.OnSpawn()
    {
        CardDrawMech.IsDrawAllowed = false;
    }
}
