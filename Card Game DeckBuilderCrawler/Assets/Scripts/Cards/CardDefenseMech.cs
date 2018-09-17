using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDefenseMech : MonoBehaviour, ICardMech
{

    private int defense;
    //the card which this defense mechanic is attached to
    private Card card;

    int ICardMech.GetValue()
    {
        return this.defense;
    }

    [SerializeField]
    private GameObject player;

    public int Defense
    {
        get
        {
            return this.defense;
        }
        set
        {
            this.defense = value;
            this.card = this.GetComponent<Card>();
            //attach the apply defense function to the apply card action event
            this.card.applyCardActionDelegate += ApplyDefense;
        }
    }

    private void ApplyDefense()
    {
        //Apply the cards defense to the player
        //GameManager.Instance.monster.TakeDamage(this.attack);
        //PlayerInteraction playerIntera = player.transform.GetComponent<PlayerInteraction>();
        //playerIntera.AddDefence(this.defense);
        PlayerInteraction.Instance.AddDefence(this.defense);
    }
}