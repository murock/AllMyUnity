using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTower : Tower {
    [SerializeField]
    private float tickTime;

    [SerializeField]
    private PoisonSplash splashPrefab;

    [SerializeField]
    private int splashDamage;

    public int SplashDamage
    {
        get
        {
            return splashDamage;
        }
    }

    public float TickTime
    {
        get
        {
            return tickTime;
        }
    }

    private void Start()
    {
        ElementType = Element.POISON;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(2,1,.5f,-0.1f,1),   // first upgrade
            new TowerUpgrade(5,1,.5f,-0.1f,1),   //second upgrade
        };
    }

    public override Debuff GetDebuff()
    {
        return new PoisonDebuff(splashDamage, tickTime, splashPrefab, DebuffDuration, Target);
    }
}
