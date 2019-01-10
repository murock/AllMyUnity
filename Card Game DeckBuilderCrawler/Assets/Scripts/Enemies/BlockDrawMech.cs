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
        bool blockMonsterStillAlive = false;
        foreach (MonsterInteraction monsterSpawner in TurnManager.Instance.monsterSpawners)
        {
            if (monsterSpawner.IsAlive && monsterSpawner.CurrentMonster != null && monsterSpawner.CurrentMonster.MonsterMech != null)
            {
                if (monsterSpawner.CurrentMonster.MonsterMech.GetToolTip() == "Cannot Draw Cards")
                {
                    blockMonsterStillAlive = true;
                }
                
            }
        }
        Debug.Log("block still alive: " + blockMonsterStillAlive);
        if (!blockMonsterStillAlive)
        {
            CardDrawMech.IsDrawAllowed = true;
        }

    }

    void IMonsterMech.OnSpawn()
    {
        CardDrawMech.IsDrawAllowed = false;
    }
}
