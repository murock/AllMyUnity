﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMultiplierMech : MonoBehaviour, ICardMech, IPersistantCard
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

    void IPersistantCard.SetTurnsToPersist(int turnsToPersist)
    {
        throw new System.NotImplementedException();
    }

    void IPersistantCard.ApplyPersistAction(Card card)
    {
        foreach (ICardMech iCard in card.iCards)
        {
            //Get the orginal card value
            int cardValue = iCard.GetValue();
            //Multiply that value
            iCard.SetValue(cardValue * GameManager.Instance.multiplierNum);
            //Apply the action with the multipled value
            card.OnApplyCardAction();
            //Return the value to its orginal number
            iCard.SetValue(cardValue);
        }

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
  
        GameManager.Instance.persistantCardArea.PassToPersistantPanel(this.transform);

       // GameManager.Instance.multiplierOn = true;
       // GameManager.Instance.multiplierNum = this.numTimesToMultiply;
        
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
