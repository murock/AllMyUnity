using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDestroyMech : MonoBehaviour, ICardMech, ICardSelectableMech {


    private int numCardsToDestroy;
    //the card which this attack mechanic is attached to
    private Card card;
    private Hand hand;
    private GameObject centrePanel;
    int ICardMech.GetValue()
    {
        return this.numCardsToDestroy;
    }

    //TODO: Implement this so the number affects how many are destroyed
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
        List<Transform> cardsInHand = new List<Transform>();
        for (int i = 0; i < this.hand.transform.childCount; i++)
        {
            Transform card = this.hand.transform.GetChild(i);
            //CardActions cardAction = card.GetComponent<CardActions>();
            //if (cardAction != null)
            //{
            //    cardAction.isDragable = false;
            //}
            //If the tag is card then child is a card
            if (card.tag == "card")
            {
                cardsInHand.Add(card);
            }
        }
        //If there are no cards in hand do nothing
        if (cardsInHand.Count > 0)
        {
            //Make panel visible
            this.centrePanel.SetActive(true);
            foreach (Transform card in cardsInHand)
            {
                //make the centrePanel the parent control of the card
                //card.SetParent(this.centrePanel.transform);
                SelectionPanel.Instance.PassToPanel(card, this, this.numCardsToDestroy);
            }
        }
    }

    void ICardSelectableMech.ApplySelectionAction(List<Transform> cardsSelected, List<Transform> cardsNotSelected)
    {
        for (int i = cardsSelected.Count - 1; i >= 0; i--)
        {
            //Would be good to have a card destroying animation show here e.g burning card
            if (DeckManager.Instance.cardsInPlay.Contains(cardsSelected[i]))
            {
                DeckManager.Instance.cardsInPlay.Remove(cardsSelected[i]);
            }
            Destroy(cardsSelected[i].gameObject);
        }
        foreach (Transform card in cardsNotSelected)
        {
            CardActions cardAction = card.GetComponent<CardActions>();
            if (cardAction != null)
            {
                cardAction.isDragable = true;
            }
            card.SetParent(this.hand.transform);
        }
    }
}
