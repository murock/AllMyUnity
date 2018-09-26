using System;
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
        initialDeckSize = 12;
        for (int i = 0; i < initialDeckSize; i++)
        {
            Card newCard;
            //Creates a new gameobject which based off the card in the Resource folder
            GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
            if (i < 5)
            {

                Color cardColor = new Color(1f, 0f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card attack mechanic to the card prefab
                CardAttackMech cardAttackMechanic = newCardPrefab.AddComponent<CardAttackMech>() as CardAttackMech;
                cardAttackMechanic.Attack = 1;
                CreateCard(newCard, newCardPrefab, "Attack", "+1 Attack", cardAttackMechanic, cardColor);
            }
            else if (5 <= i && 8 > i)
            {
                Color cardColor = new Color(0f, 250f, 148f, 255f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card defense mechanic to the card prefab
                CardDefenseMech cardDefenseMechanic = newCardPrefab.AddComponent<CardDefenseMech>() as CardDefenseMech;
                cardDefenseMechanic.Defense = 1;
                CreateCard(newCard, newCardPrefab, "Defense", "+1 Defense", cardDefenseMechanic, cardColor);
            }

            //Multiplier test
            else if (8 <= i && 10 > i)
            {
                Color cardColor = new Color(145f, 0f, 148f, 255f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card defense mechanic to the card prefab
                CardMultiplierMech cardMultiplierMechanic = newCardPrefab.AddComponent<CardMultiplierMech>() as CardMultiplierMech;
                cardMultiplierMechanic.NumTimesToMultiply = 2; ////NUm cards = mUltiply x n
                CreateCard(newCard, newCardPrefab, "Multiplier", "x2 Multiply", cardMultiplierMechanic, cardColor);
            } 


            else
            {
                //CARD DRAW --------------------
                //Color cardColor = new Color(250f, 69f, 0f, 1f);
                //newCard = newCardPrefab.AddComponent<Card>() as Card;
                //CreateCard(newCard,newCardPrefab,"Draw", "+2 Draw", 0, cardColor);
                ////attach the card draw mechanic to the card prefab
                //CardDrawMech cardDrawMechanic = newCardPrefab.AddComponent<CardDrawMech>() as CardDrawMech;
                //cardDrawMechanic.NumCardsToDraw = 2;

                //CARD DESTROY ---------------------------
                //Color cardColor = new Color(255f, 255f, 255f, 1f);
                //newCard = newCardPrefab.AddComponent<Card>() as Card;
                ////attach the card draw mechanic to the card prefab
                //CardDestroyMech cardDestroyMechanic = newCardPrefab.AddComponent<CardDestroyMech>() as CardDestroyMech;
                //cardDestroyMechanic.NumCardsToDestroy = 2;
                //CreateCard(newCard, newCardPrefab, "Destroy", "+2 Destroy", cardDestroyMechanic, cardColor);

                //CARD MONEY ---------------------------
                Color cardColor = new Color(0, 1f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card draw mechanic to the card prefab
                CardMoneyMech cardMoneyMechanic = newCardPrefab.AddComponent<CardMoneyMech>() as CardMoneyMech;
                cardMoneyMechanic.Money = 1;
                CreateCard(newCard, newCardPrefab, "Cash", "+1 Cash", cardMoneyMechanic, cardColor);
            }
        }
        //give a copy of the deck to the card draw class
        CardDraw.Instance.deck = this.cardsInDeck;
    }

   
    private void CreateCard(Card cardType, GameObject cardPrefab,string cardTitle, string cardDescription, ICardMech iCard, Color cardColor)
    {
        cardType.PopulateCard(cardTitle, cardDescription, iCard, cardColor);
        // making the deck its parent
        cardType.transform.SetParent(this.transform);
        //add the card to the global list
        cardsInDeck.Add(cardType.transform);
    }

    internal void AddCardToDeck(Transform card)
    {
        cardsInDeck.Add(card);
        card.transform.SetParent(this.transform);
        CardActions cardAction = card.GetComponent<CardActions>();
        if (cardAction != null)
        {
            cardAction.InstantFade();
        }

    }
}
