using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    //Each turn
    //1) draw 5 cards
    //2) Play cards
    //3) End Turn
    //4) Monster Attacks
    //5) Check if Monster/Player Dead if so end game
    //6) Repeat 1 to 5

    public TurnManager()
    {
        //1) draw 5 cards
        for (int i = 0; i < 5; i++)
        {
            CardDraw.Instance.drawCard();
        }
    }

}
