using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDestroyMech : MonoBehaviour {


    private int numCardsToDestroy;
    //the card which this attack mechanic is attached to
    private Card card;
    private Hand hand;
    private GameObject centrePanel;

    public int NumCardsToDestroy
    {
        get
        {
            return this.numCardsToDestroy;
        }
        set
        {
            this.numCardsToDestroy = value;
            this.hand = GameManager.Instance.hand;
            this.centrePanel = GameManager.Instance.centrePanel;
            this.card = this.GetComponent<Card>();
            //attach the apply attack function to the apply card action event
            this.card.applyCardActionDelegate += ApplyDestroyCards;
        }
    }

    private void ApplyDestroyCards()
    {
        //Destroy Cards

    }
}
