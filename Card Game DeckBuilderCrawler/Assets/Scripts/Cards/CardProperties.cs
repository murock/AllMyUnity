using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardProperties : MonoBehaviour {

    [SerializeField]
    private int attack, defence;

    public int Attack
    {
        get
        {
            return this.attack;
        }
        set
        {
            this.attack = value;
        }      
    }

    public int Defense
    {
        get
        {
            return this.defence;
        }
        set
        {
            this.defence = value;
        }
    }
}
