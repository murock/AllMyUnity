using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMultiplierMech : MonoBehaviour, ICardMech
{


    private int numTimesToMultiply;
    //the card which this multiply mechanic is attached to
    private Card card;

    int ICardMech.GetValue()
    {
        return this.numTimesToMultiply;
    }

    void ICardMech.SetValue(int value)
    {
        this.numTimesToMultiply = value;
    }


    public int NumTimesToMultiply
    {
        get
        {
            return this.numTimesToMultiply;
        }
        set
        {
            this.numTimesToMultiply = value;
            this.card = this.GetComponent<Card>();
            //attach the apply multipy function to the apply card action event
            this.card.applyCardActionDelegate += ApplyMultiplier;
            
        }
    }

    public bool MultiplyOn
    {
        get
        {
            return this.multiplyon;
        }
        set
        {
            this.multiplyon = value;
        }
    }

    bool multiplyon = false;


    public bool CheckOn()
    {
        if (numTimesToMultiply > 0)
        {
            multiplyon = true;
            return multiplyon;
        }
        return multiplyon;
    }

   


    public void ApplyMultiplier()
    {

        GameManager.Instance.multiplierOn = true;
        GameManager.Instance.multiplierNum = this.numTimesToMultiply;
        //Multicheck = true;

        //for (int i = this.numCardsToMultiply; i < 0; i--)
        // {
        // if (i > 0)
        // {
        //    multiplyon = true;
        //}
        //}

    }
    
    
}
