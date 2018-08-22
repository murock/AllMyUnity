using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager> {

    //Each turn
    //1) draw 5 cards
    //2) Play cards -- monster dead check - done in MonsterInteraction
    //3) End Turn
    //4) Monster Attacks
    //5) Check if Monster/Player Dead if so end game
    //6) Repeat 1 to 5
    private int handSize = 5;

    [SerializeField]
    MonsterInteraction monster;
    public TurnManager()
    {       
    }

    public IEnumerator DrawInitialHand()
    {
        //1) draw 5 cards
        int i = 0;
        while (i < this.handSize)
        {
            CardDraw.Instance.drawCard();
            i++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void EndTurn()
    {
        monster.DoDamage();
    }
}
