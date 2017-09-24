using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower {

    [SerializeField]
    private float tickTime;

    [SerializeField]
    private float tickDamage;

    public float TickTime
    {
        get
        {
            return tickTime;
        }

    }

    public float TickDamage
    {
        get
        {
            return tickDamage;
        }
    }

    private void Start()
    {
        ElementType = Element.FIRE;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(2,2,.5f,5,-0.1f,1),    //first upgrade
            new TowerUpgrade(5,3,.5f,5,-0.1f,1),    //second upgrade
        };
    }

    public override Debuff GetDebuff()
    {
        return new FireDebuff(tickDamage, tickTime, DebuffDuration, Target);
    }
}
