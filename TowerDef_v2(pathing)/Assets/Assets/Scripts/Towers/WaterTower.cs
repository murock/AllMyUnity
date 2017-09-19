using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTower : Tower {

    [SerializeField]
    private float slowingFactor;

    private void Start()
    {
        ElementType = Element.WATER;
    }

    public override Debuff GetDebuff()
    {
        return new WaterDebuff(slowingFactor, DebuffDuration,Target);
    }
}
