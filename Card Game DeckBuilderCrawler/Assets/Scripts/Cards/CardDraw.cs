using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDraw : Singleton<CardDraw>{

    [SerializeField]
    private GameObject deckLabel;

    [SerializeField]
    private GameObject hand;

    public List<Transform> deck;

    public bool drawCard()
    {
        bool cardAdded = false;
        //cards still left to draw
        if (deck.Count > 0)
        {           
            int randomNumber = Random.Range(0, deck.Count - 1);
            //Make the hand the parent of the card
            deck[randomNumber].transform.parent = hand.transform;
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
            deckLabel.GetComponent<Text>().text = "Deck" + System.Environment.NewLine + "Cards Left: " + deck.Count.ToString();
        }
        else
        {
            deckLabel.GetComponent<Text>().text = "Deck" + System.Environment.NewLine +  "Out of Cards";
            //Put cards back into the deck
            List<Transform> cardsToReshuffle = new List<Transform>();
            for (int i = 0; i < GameManager.Instance.transform.childCount; i++)
            {
                Transform card = GameManager.Instance.transform.GetChild(i);
                cardsToReshuffle.Add(card);
            }
            for (int i = 0; i < PlayArea.Instance.transform.childCount; i++)
            {
                Transform card = PlayArea.Instance.transform.GetChild(i);
                cardsToReshuffle.Add(card);
            }
            reShuffleCards(cardsToReshuffle);
        }
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

    public void drawCardButtonHOTFIX()
    {
        drawCard();
    }

    private void reShuffleCards(List<Transform> cards)
    {
        //Need to reorganise this so it goes through the cardsdiscarded
        foreach (Transform card in cards)
        {
            //If the tag is card then is a card
            if (card.tag == "card")
            {
                deck.Add(card);
                Draggable d = card.GetComponent<Draggable>();

                //CanvasGroup cardCanvasGroup = card.GetComponent<CanvasGroup>();
                ////visible
                //cardCanvasGroup.alpha = 1;
                ////interactable
                //cardCanvasGroup.blocksRaycasts = true;
                d.parentToReturnTo = this.transform;
                //  d.isDiscarded = false;
                d.ShuffleCardBack();
            }
        }
        deckLabel.GetComponent<Text>().text = "Deck" + System.Environment.NewLine + "Cards Left: " + deck.Count.ToString();
        DeckManager.Instance.cardsInDeck = this.deck;
    }
}
