using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower : Tower {

    [SerializeField]
    private float slowingFactor;

    public float SlowingFactor
    {
        get
        {
            return slowingFactor;
        }
    }

    private void Start()
    {
        ElementType = Element.WATER;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(2,1,1,2,10),   // first upgrade
            new TowerUpgrade(2,1,1,2,20),   //second upgrade
        };
    }

    public override Debuff GetDebuff()
    {
        return new WaterDebuff(SlowingFactor, DebuffDuration,Target);
    }
}
