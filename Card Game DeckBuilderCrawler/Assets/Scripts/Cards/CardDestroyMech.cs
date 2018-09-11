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
        //Make panel visible
        this.centrePanel.SetActive(true);
        List<Transform> cardsInHand = new List<Transform>();
        for (int i = 0; i < this.hand.transform.childCount; i++)
        {
            Transform card = this.hand.transform.GetChild(i);
            //If the tag is card then child is a card
            if (card.tag == "card")
            {
                cardsInHand.Add(card);
            }
        }
        foreach (Transform card in cardsInHand)
        {
            //make the centrePanel the parent control of the card
            card.SetParent(this.centrePanel.transform);
        }
        //Make panel invisible
        //this.centrePanel.SetActive(false);
    }
}
