using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAttackMech : MonoBehaviour {

    private int attack;
    //the card which this attack mechanic is attached to
    private Card card;

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
        GameManager.Instance.monster.TakeDamage(this.attack);
    }
}
