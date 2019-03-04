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

    [SerializeField]
    private Transform handLocation;

    //Local copy of the deck
    public List<Transform> deck;

    private const int handSize = 5;

    [SerializeField]
    private Button turnButton;

    //Returns true if a card was added to the hand
    public bool drawCard()
    {
        bool cardAdded = false;
        // cards still left to draw
        if (deck.Count > 0)
        {
            // random number between 0 and the max deck size
            int randomNumber = Random.Range(0, deck.Count - 1);
            // Make the hand the parent of the card
            StartCoroutine(MoveFromTo(deck[randomNumber],deck[randomNumber].position, handLocation.position, 750));

            // Make visible and clickable
            CanvasGroup cardCanvasGroup = deck[randomNumber].GetComponent<CanvasGroup>();
            cardCanvasGroup.alpha = 1;
            cardCanvasGroup.blocksRaycasts = true;

            // Add the card to the list of cards in play
            DeckManager.Instance.cardsInPlay.Add(deck[randomNumber]);
            // remove card from deck list
            deck.RemoveAt(randomNumber);
            if (DeckManager.Instance.cardsInPlay.Count > Hand.handSize)
            {
                Hand.Instance.layoutGroup.spacing -= 10f;
            }
            cardAdded = true;
        }
        if (deck.Count > 0)
        {
            deckLabel.text = "Deck" + System.Environment.NewLine + "Cards Left: " + deck.Count.ToString();
        }
        else
        {
            deckLabel.text = "Deck" + System.Environment.NewLine +  "Out of Cards";
            // Put cards back into the deck
            reShuffleCards();
        }
        // Update global deck list
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

    public IEnumerator DrawCards(int numCardsToDraw = handSize)
    {
        //1) draw 5 cards
        int i = 0;
        //DANGER INFINITE LOOP as i not always incremented CHECK THIS LOGIC (temp)
        while (i < numCardsToDraw)
        {
            yield return new WaitForSeconds(0.25f);
            //if a card was drawn increment i
            if (CardDraw.Instance.drawCard())
            {
                i++;
            }
            //if there are no cards left to draw increment i
            else if (DeckManager.Instance.cardsInDeck.Count == 0)
            {
                i++;
            }
        }
        this.turnButton.enabled = true;
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

    IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
        Hand.Instance.dealCardToHand(objectToMove);
    }
}
