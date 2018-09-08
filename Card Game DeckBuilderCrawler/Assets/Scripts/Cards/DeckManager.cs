using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : Singleton<DeckManager> {


    private int initialDeckSize = 10;

    internal List<Transform> cardsInPlay;
    internal List<Transform> cardsDiscarded;
    internal List<Transform> cardsInDeck;

    // Use this for initialization
    void Awake () {
        //3 global card lists
        cardsInPlay = new List<Transform>();
        cardsDiscarded = new List<Transform>();
        cardsInDeck = new List<Transform>();
        //temp change deck size for testing
        initialDeckSize = 11;
        for (int i = 0; i < initialDeckSize; i++)
        {
            Card newCard;
            //Creates a new gameobject which based off the card in the Resource folder
            GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
            if (i < 5)
            {
                Color cardColor = new Color(1f, 0f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                newCard.PopulateCard("Attack", "+1 Attack", 1, 0, 0, cardColor);
            }
            else if ( 5 <= i && 8 > i)
            {
                Color cardColor = new Color32(0, 250, 148, 255);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                newCard.PopulateCard("Defense", "+1 Defense", 0, 1, 0, cardColor);
            }
            else
            {
                Color cardColor = new Color(250f, 69f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                newCard.PopulateCard("Draw", "+2 Draw", 0, 0, 2, cardColor);
            }

            // making the deck its parent
            newCard.transform.SetParent(this.transform);
            CanvasGroup cardCanvasGroup = newCard.GetComponent<CanvasGroup>();
            //making it not visible
            cardCanvasGroup.alpha = 0;
            //not interactable
            cardCanvasGroup.blocksRaycasts = false;
            //add the card to the global list
            cardsInDeck.Add(newCard.transform);

        }
        //give a copy of the deck to the card draw class
        CardDraw.Instance.deck = this.cardsInDeck;

	}
	
}
