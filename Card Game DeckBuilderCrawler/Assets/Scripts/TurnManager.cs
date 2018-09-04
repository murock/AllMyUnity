using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : Singleton<TurnManager> {

    //Each turn
    //1) draw 5 cards
    // mulligan cards?
    //2) Play cards -- monster dead check - done in MonsterInteraction
    //3) End Turn - Discard remaining cards
    //4) Monster Attacks
    //5) Check if Monster/Player Dead if so end game
    //6) Repeat 1 to 5
    private int handSize = 5;


    [SerializeField]
    MonsterInteraction monster;
    public TurnManager()
    {       
    }

    public IEnumerator DrawHand()
    {
        //1) draw 5 cards
        int i = 0;
        //DANGER INFINITE LOOP as i not always incremented CHECK THIS LOGIC
        while (i < this.handSize)
        {
            //if a card was drawn increment i
            if (CardDraw.Instance.drawCard())
            {
                i++;
            }
            //if there are no cards left to draw increment i
            else if (DeckManager.Instance.cardsInDeck.Count == 0)
            {
                i++;
            }        
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void EndTurn()
    {
        Hand.Instance.DiscardHand();
        monster.DoDamage();
        StartCoroutine(DrawHand());
    }

    private void Mulligan()
    {

    }
}
