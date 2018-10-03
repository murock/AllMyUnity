using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDiscardMech : MonoBehaviour, ICardMech, ICardSelectableMech
{
    private int numCardsToDiscard;
    //the card which this attack mechanic is attached to
    private Card card;
    private Hand hand;
    private GameObject centrePanel;
    int ICardMech.GetValue()
    {
        return numCardsToDiscard;
    }

    void ICardMech.SetValue(int value)
    {
        this.numCardsToDiscard = value;
    }

    public int NumCardsToDiscard
    {
        get
        {
            return this.numCardsToDiscard;
        }
        set
        {
            this.numCardsToDiscard = value;
            this.hand = GameManager.Instance.hand;
            this.centrePanel = GameManager.Instance.centrePanel;
            this.card = this.GetComponent<Card>();
            //attach the apply destroy function to the apply card action event
            this.card.applyCardActionDelegate += ApplyDestroyCards;
        }
    }

    private void ApplyDestroyCards()
    {
        List<Transform> cardsInHand = new List<Transform>();
        for (int i = 0; i < this.hand.transform.childCount; i++)
        {
            Transform card = this.hand.transform.GetChild(i);
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
            SelectionPanel.Instance.titleLabel.text = "Discard Cards";
            foreach (Transform card in cardsInHand)
            {
                SelectionPanel.Instance.PassToPanel(card, this, this.numCardsToDiscard);
            }
        }
    }

    void ICardSelectableMech.ApplySelectionAction(List<Transform> cardsSelected, List<Transform> cardsNotSelected)
    {
        for (int i = cardsSelected.Count - 1; i >= 0; i--)
        {
            //Would be good to have a card discarding animation show here e.g cards flying into deck
            if (DeckManager.Instance.cardsInPlay.Contains(cardsSelected[i]))
            {
                DeckManager.Instance.cardsInPlay.Remove(cardsSelected[i]);
                DeckManager.Instance.AddCardToDeck(cardsSelected[i]);
            }
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
        for (int i = 0; i < cardsSelected.Count; i++)
        {
            CardDraw.Instance.drawCard();
        }
    }


}
