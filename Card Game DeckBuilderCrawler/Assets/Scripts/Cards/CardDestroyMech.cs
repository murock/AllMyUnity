using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDestroyMech : MonoBehaviour, ICardSelectableMech {


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
            //attach the apply destroy function to the apply card action event
            this.card.applyCardActionDelegate += ApplyDestroyCards;
        }
    }

    private void Card_applyCardSelectionActionDelegate()
    {
        Destroy(this.gameObject);
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
            CardActions cardAction = card.GetComponent<CardActions>();
            if (cardAction != null)
            {
                cardAction.isDragable = false;
            }
            //If the tag is card then child is a card
            if (card.tag == "card")
            {
                cardsInHand.Add(card);
            }
        }
        foreach (Transform card in cardsInHand)
        {
            //make the centrePanel the parent control of the card
            //card.SetParent(this.centrePanel.transform);
            SelectionPanel.Instance.PassToPanel(card, this);
        }
        //Make panel invisible
        //this.centrePanel.SetActive(false);
    }

    void ICardSelectableMech.ApplySelectionAction(List<Transform> cardsSelected, List<Transform> cardsNotSelected)
    {
        for (int i = cardsSelected.Count - 1; i >= 0; i--)
        {
            //Would be good to have a card destroying animation show here e.g burning card
            Destroy(cardsSelected[i].gameObject);
        }
        foreach (Transform card in cardsNotSelected)
        {
            card.SetParent(this.hand.transform);
        }
    }
}
