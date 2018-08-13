using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardProperties : MonoBehaviour {

    [SerializeField]
    private int attack;

    public int Attack
    {
        get
        {
            return this.attack;
        }      
    }
}
