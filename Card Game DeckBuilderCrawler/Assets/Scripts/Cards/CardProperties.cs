using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardProperties : MonoBehaviour {

    [SerializeField]
    private int defence;


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
