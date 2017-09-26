using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTower : Tower {

    private void Start()
    {
        ElementType = Element.EARTH;

        Upgrades = new TowerUpgrade[]
        {
            new TowerUpgrade(2,2,1,2),   // first upgrade
            new TowerUpgrade(5,3,1,2),   //second upgrade
        };
    }

    public override Debuff GetDebuff()
    {
        return new EarthDebuff(Target, DebuffDuration);
    }

    public override string GetStats()
    {
        return string.Format("<color=#8b4513ff>{0}</color>{1}", "<size=20><b>Earth</b></size>", base.GetStats());
    }
}
