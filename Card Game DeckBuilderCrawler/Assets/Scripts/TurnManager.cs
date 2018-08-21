using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    //Each turn
    //1) draw 5 cards
    //2) Play cards -- monster dead check here??
    //3) End Turn
    //4) Monster Attacks
    //5) Check if Monster/Player Dead if so end game
    //6) Repeat 1 to 5
    private int handSize = 5;
    public TurnManager()
    {
        //1) draw 5 cards
        
    }

    public IEnumerator DrawInitialHand()
    {
        int i = 0;
        while (i < this.handSize)
        {
            CardDraw.Instance.drawCard();
            i++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
