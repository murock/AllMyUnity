using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTower : Tower {

    private void Start()
    {
        ElementType = Element.EARTH;
    }

    public override Debuff GetDebuff()
    {
        return new EarthDebuff(Target, DebuffDuration);
    }
}
