using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMultiplierMech : MonoBehaviour, ICardMech, IPersistantCard
{

    private const string toolTip = "The next card you play will trigger its action more than once";
    internal int numTimesToMultiply;
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

    string ICardMech.ToolTipText()
    {
        return toolTip;
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
        GameManager.Instance.persistantCardArea.RemoveFromPanel(this.transform);
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

    public CardMultiplierMech()
    {
        //CardActions cardAction = this.GetComponent<CardActions>();
        //if (cardAction != null)
        //{
        //    cardAction.AttachTooltip(toolTip);
        //}
    }

    public void ApplyMultiplier()
    {
        
        //GameManager.Instance.persistantCardArea.PassToPersistantPanel(this.transform);

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
