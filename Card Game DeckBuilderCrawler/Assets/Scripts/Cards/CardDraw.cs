using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardDraw : Singleton<CardDraw>{
    //Label on top of the deck
    [SerializeField]
    private Text deckLabel;

    [SerializeField]
    private GameObject hand;

    //Local copy of the deck
    public List<Transform> deck;

    //Returns true if a card was added to the hand
    public bool drawCard()
    {
        bool cardAdded = false;
        //cards still left to draw
        if (deck.Count > 0)
        {
            //random number between 0 and the max deck size
            int randomNumber = Random.Range(0, deck.Count - 1);
            //Make the hand the parent of the card
            deck[randomNumber].transform.SetParent(hand.transform);
            //Make visible and clickable
            CanvasGroup cardCanvasGroup = deck[randomNumber].GetComponent<CanvasGroup>();
            cardCanvasGroup.alpha = 1;
            cardCanvasGroup.blocksRaycasts = true;

            //Add the card to the list of cards in play
            DeckManager.Instance.cardsInPlay.Add(deck[randomNumber]);
            //remove card from deck list
            deck.RemoveAt(randomNumber);
            cardAdded = true;
        }
        if (deck.Count > 0)
        {
            deckLabel.text = "Deck" + System.Environment.NewLine + "Cards Left: " + deck.Count.ToString();
        }
        else
        {
            deckLabel.text = "Deck" + System.Environment.NewLine +  "Out of Cards";
            //Put cards back into the deck
            reShuffleCards();
        }
        //Update global deck list
        DeckManager.Instance.cardsInDeck = this.deck;

        if (cardAdded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Temporary function to draw cards by clicking on the deck
    public void drawCardButtonHOTFIX()
    {
        drawCard();
    }

    //reshuffles all the cards that have been discarded
    private void reShuffleCards()
    {
        //go through every card in the discarded list and add it to the deck
        foreach (Transform card in DeckManager.Instance.cardsDiscarded)
        {
            deck.Add(card);
            CardActions d = card.GetComponent<CardActions>();
            d.parentToReturnTo = this.transform;
            d.ShuffleCardBack();
        }
        //remove cards form cards discarded list since they are all now back in the deck
        DeckManager.Instance.cardsDiscarded.Clear();
        deckLabel.text = "Deck" + System.Environment.NewLine + "Cards Left: " + deck.Count.ToString();
        //update the global list of cards in the deck
        DeckManager.Instance.cardsInDeck = this.deck;
    }

    //Should maybe move to deck manager?
    public void UpdateLabel()
    {
        if (DeckManager.Instance.cardsInDeck.Count > 0)
        {
            deckLabel.text = "Deck" + System.Environment.NewLine + "Cards Left: " + DeckManager.Instance.cardsInDeck.Count.ToString();
        }
        else
        {
            deckLabel.text = "Deck" + System.Environment.NewLine + "Out of Cards";
        }

    }
}
