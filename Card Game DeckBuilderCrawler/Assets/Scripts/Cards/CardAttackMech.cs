using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttackMech : MonoBehaviour, ICardMech {

    private int attack;
    //the card which this attack mechanic is attached to
    private Card card;
    private const string toolTip = "Attacks a monster";

    int ICardMech.GetValue()
    {
        return this.attack;
    }

    void ICardMech.SetValue(int value)
    {
        this.attack = value;
    }

    string ICardMech.ToolTipText()
    {
        return toolTip;
    }

    public int Attack
    {
        get
        {
            return this.attack;
        }
        set
        {
            this.attack = value;
            this.card = this.GetComponent<Card>();
            //attach the apply attack function to the apply card action event
            this.card.applyCardActionDelegate += ApplyAttack;
        }
    }

    private void ApplyAttack()
    {
        //Apply the cards attack to the monster
        if (GameManager.Instance.currentMonster != null &&
            GameManager.Instance.currentMonster.IsAlive)
        {
            GameManager.Instance.currentMonster.TakeDamage(this.attack);
        }
    }


}
