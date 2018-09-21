using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawMech : MonoBehaviour, ICardMech {

    private int numCardsToDraw;
    //the card which this attack mechanic is attached to
    private Card card;

    int ICardMech.GetValue()
    {
        return this.numCardsToDraw;
    }

    void ICardMech.SetValue(int value)
    {
        this.numCardsToDraw = value;
    }

    public int NumCardsToDraw
    {
        get
        {
            return this.numCardsToDraw;
        }
        set
        {
            this.numCardsToDraw = value;
            this.card = this.GetComponent<Card>();
            //attach the apply attack function to the apply card action event
            this.card.applyCardActionDelegate += ApplyCardDraw;
        }
    }

    private void ApplyCardDraw()
    {
        // Draw cards
        for (int i = 0; i < this.numCardsToDraw; i++)
        {
            CardDraw.Instance.drawCard();
        }
    }
}
