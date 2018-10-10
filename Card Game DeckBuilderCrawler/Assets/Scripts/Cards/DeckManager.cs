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
        initialDeckSize = 10;
        for (int i = 0; i < initialDeckSize; i++)
        {
            Card newCard;
            //Creates a new gameobject which based off the card in the Resource folder
            GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
            if (i < 4)
            {

                Color cardColor = new Color(1f, 0f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card attack mechanic to the card prefab
                CardAttackMech cardAttackMechanic = newCardPrefab.AddComponent<CardAttackMech>() as CardAttackMech;
                cardAttackMechanic.Attack = 1;
                CreateCard(newCard, newCardPrefab, "Attack", "+1 Attack", 0, cardAttackMechanic, cardColor);
            }
            else if (i < 7)
            {
                Color cardColor = new Color(0f, 250f, 148f, 255f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card defense mechanic to the card prefab
                CardDefenseMech cardDefenseMechanic = newCardPrefab.AddComponent<CardDefenseMech>() as CardDefenseMech;
                cardDefenseMechanic.Defense = 1;
                CreateCard(newCard, newCardPrefab, "Defense", "+1 Defense", 0, cardDefenseMechanic, cardColor);
            }
            else
            {
                //CARD MONEY ---------------------------
                Color cardColor = new Color(0, 1f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card draw mechanic to the card prefab
                CardMoneyMech cardMoneyMechanic = newCardPrefab.AddComponent<CardMoneyMech>() as CardMoneyMech;
                cardMoneyMechanic.Money = 1;
                CreateCard(newCard, newCardPrefab, "Cash", "+1 Cash", 0, cardMoneyMechanic, cardColor);
            }
                //Multiplier test
                //Color cardColor = new Color(145f, 0f, 148f, 255f);
                //newCard = newCardPrefab.AddComponent<Card>() as Card;
                ////attach the card defense mechanic to the card prefab
                //CardMultiplierMech cardMultiplierMechanic = newCardPrefab.AddComponent<CardMultiplierMech>() as CardMultiplierMech;
                //cardMultiplierMechanic.NumTimesToMultiply = 2; ////NUm cards = mUltiply x n
                //CreateCard(newCard, newCardPrefab, "Multiplier", "x2 Multiply", 2, cardMultiplierMechanic, cardColor);

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

                //CARD DISCARD ---------------------------
                //Color cardColor = new Color(0, 1f, 0f, 1f);
                //newCard = newCardPrefab.AddComponent<Card>() as Card;
                ////attach the card draw mechanic to the card prefab
                //CardDiscardMech cardDiscardMechanic = newCardPrefab.AddComponent<CardDiscardMech>() as CardDiscardMech;
                //cardDiscardMechanic.NumCardsToDiscard = 4;
                //CreateCard(newCard, newCardPrefab, "Discard", "+4 Discard", 2, cardDiscardMechanic, cardColor);
            
        }
        //give a copy of the deck to the card draw class
        CardDraw.Instance.deck = this.cardsInDeck;
    }

   
    private void CreateCard(Card cardType, GameObject cardPrefab,string cardTitle, string cardDescription, int cardCost, ICardMech iCard, Color cardColor)
    {
        cardType.PopulateCard(cardTitle, cardDescription, cardCost, iCard, cardColor);
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
        CardDraw.Instance.UpdateLabel();
    }
}
