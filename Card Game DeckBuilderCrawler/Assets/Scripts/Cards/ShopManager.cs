using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopManager : Singleton<ShopManager>
{
    List<Transform> CashCards;
    public bool isShopping;
    private bool isCashShopping = false;
    [SerializeField]
    private Button cashShopButton;
    [SerializeField]
    private Text cashShopButtonText;
    private int numUniqueCards = 5;
    private Queue<Transform> CardsInShopDeck;
    private List<Transform> CardsOnDisplay = new List<Transform>();
    private int numCardsToDisplay = 4;


    public void PopulateShop()
    {
        // If cardsTodisplay.count == numCardsToDisplay then show panel with cards
        // else if cardsTodisplay.count < numcardstodisplay then add cards from cardsinshopdeck untill == to count or no cards left in cardsinshopdeck then display
        while (CardsOnDisplay.Count < numCardsToDisplay && CardsInShopDeck.Count != 0 )
        {
            CardsOnDisplay.Add(CardsInShopDeck.Dequeue());
        }
    }

    private void AddCardToShopDeck()
    {
        int randNum = Random.Range(0, numUniqueCards);
        Card newCard;
        //Creates a new gameobject which based off the card in the Resource folder
        GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
        if (randNum == 0)
        {
            //DESTROY
            Color cardColor = new Color(255f, 255f, 255f, 1f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card draw mechanic to the card prefab
            CardDestroyMech cardDestroyMechanic = newCardPrefab.AddComponent<CardDestroyMech>() as CardDestroyMech;
            cardDestroyMechanic.NumCardsToDestroy = 2;
            newCard.PopulateCard("Destroy", "+2 Destroy", 2, cardDestroyMechanic, cardColor, "CardArt/Defense");
        }
        else if (randNum == 1)
        {
            //DRAW
            Color cardColor = new Color(250f, 69f, 0f, 1f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card draw mechanic to the card prefab
            CardDrawMech cardDrawMechanic = newCardPrefab.AddComponent<CardDrawMech>() as CardDrawMech;
            cardDrawMechanic.NumCardsToDraw = 2;
            newCard.PopulateCard("Draw", "+2 Draw", 1, cardDrawMechanic, cardColor, "CardArt/Defense");
        }
        else if (randNum == 2)
        {
            //MULTIPLIER
            Color cardColor = new Color(145f, 0f, 211f, 255f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card defense mechanic to the card prefab
            CardMultiplierMech cardMultiplierMechanic = newCardPrefab.AddComponent<CardMultiplierMech>() as CardMultiplierMech;
            cardMultiplierMechanic.NumTimesToMultiply = 2; ////NUm cards = mUltiply x n
            newCard.PopulateCard("Multiplier", "x2 Multiply", 2, cardMultiplierMechanic, cardColor, "CardArt/Defense", Card.CardType.Persistant);
        }
        else if (randNum == 3)
        {
            //+Attack and Defence
            Color cardColor = new Color(145f, 50f, 200f, 255f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card defense mechanic to the card prefab
            CardAttackMech cardAttackMechanic = newCardPrefab.AddComponent<CardAttackMech>() as CardAttackMech;
            cardAttackMechanic.Attack = 2;
            newCard.PopulateCard("Good Offence", "+2 Attack" + Environment.NewLine + "+2 Defence", 4, cardAttackMechanic, cardColor, "CardArt/Defense");
            CardDefenseMech cardDefenceMechanic = newCardPrefab.AddComponent<CardDefenseMech>() as CardDefenseMech;
            cardDefenceMechanic.Defense = 2;
            newCard.AddAddtionalMech(cardDefenceMechanic);
        }
        else if (randNum == 4)
        {
            //+ 3 Attack
            Color cardColor = new Color(1f, 0f, 0f, 1f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card attack mechanic to the card prefab
            CardAttackMech cardAttackMechanic = newCardPrefab.AddComponent<CardAttackMech>() as CardAttackMech;
            cardAttackMechanic.Attack = 3;
            newCard.PopulateCard("Attack", "+3 Attack", 3, cardAttackMechanic, cardColor, "CardArt/Attack");
        }
        else
        {
            //CARD DISCARD ---------------------------
            Color cardColor = new Color(0, 1f, 0f, 1f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card draw mechanic to the card prefab
            CardDiscardMech cardDiscardMechanic = newCardPrefab.AddComponent<CardDiscardMech>() as CardDiscardMech;
            cardDiscardMechanic.NumCardsToDiscard = 4;
            newCard.PopulateCard("Discard", "+4 Discard", 2, cardDiscardMechanic, cardColor, "CardArt/Defense");
        }
        CardsInShopDeck.Enqueue(newCard.transform);
    }

    public void MakeStartingShopDeck(int size)
    {
        CardsInShopDeck = new Queue<Transform>();
        for (int i = 0; i < size; i++)
        {
            AddCardToShopDeck();
        }
    }

    public void StartShop()
    {
        this.cashShopButton.gameObject.SetActive(true);
        isShopping = true;
        SelectionPanel.Instance.titleLabel.text = "Shop";
        this.PopulateShop();
        this.PassCardsToCentre(this.CardsOnDisplay);
    }

    public void BuyCards(List<Transform> boughtCards)
    {
        foreach (Transform card in boughtCards)
        {
            card.gameObject.SetActive(true);
            CardActions cardAction = card.GetComponent<CardActions>();
            if (cardAction != null)
            {
                cardAction.isDragable = true;
                cardAction.isSelected = false;
            }
            DeckManager.Instance.AddCardToDeck(card);
            if (this.CardsOnDisplay.Contains(card))
            {
                // Remove card from the list of cards in the shop
                this.CardsOnDisplay.Remove(card);
            }
            else if (this.CashCards.Contains(card))
            {
                // Remove card from list of cards in cash shop
                this.CashCards.Remove(card);
                // Replace the money card in the shop
                int moneyValue = card.GetComponent<CardMoneyMech>().Money;
                Transform replacementMoneyCard = CreateMoneyCard(moneyValue).transform;
                //this.CashCards.Add(replacementMoneyCard);
                this.CashCards.Insert(moneyValue - 1, replacementMoneyCard);
                Debug.Log("removed card from cash shop");
            }
        }
        this.isShopping = false;
        this.isCashShopping = false;
        this.cashShopButtonText.text = "Coin" + Environment.NewLine + "Shop";
        CardDraw.Instance.UpdateLabel();
        this.cashShopButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Pass the card back to the shop so it doesn't linger in the centre panel
    /// </summary>
    public void PassBackToShop(List<Transform> cards)
    {
        foreach (Transform card in cards)
        {
            card.gameObject.SetActive(false);
            card.SetParent(this.transform);
        }
    }

    private void PassCardsToCentre(List<Transform> cards)
    {
        // Move the cards to the centre panel
        foreach (Transform card in cards)
        {
            card.gameObject.SetActive(true);
            CanvasGroup cardCanvasGroup = card.GetComponent<CanvasGroup>();
            if (cardCanvasGroup != null)
            {
                cardCanvasGroup.alpha = 1;
                cardCanvasGroup.blocksRaycasts = true;
            }
            SelectionPanel.Instance.PassToPanel(card, null);
        }
    }

    public void SwitchShops()
    {
        if (!isCashShopping)
        {
            // Switch to cash only shop
            this.isCashShopping = true;
            this.cashShopButtonText.text = "Back";
            this.PassBackToShop(this.CardsOnDisplay);
            if (this.CashCards == null)
            {
                this.CashCards = new List<Transform>();
                int numUniqueCards = 3;
                for (int i = 0; i < numUniqueCards; i++)
                {
                    Card newCard;
                    //Creates a new gameobject which based off the card in the Resource folder
                    GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
                    newCard = CreateMoneyCard(i + 1);
                    // making the centrePanel its parent
                    this.CashCards.Add(newCard.transform);
                }
            }
            this.PassCardsToCentre(this.CashCards);
        }
        else
        {
            // Switch to normal shop
            this.cashShopButtonText.text = "Coin" + Environment.NewLine + "Shop";
            this.isCashShopping = false;
            // Hide the cash cards
            //foreach (Transform card in this.CashCards)
            //{
            //    card.gameObject.SetActive(false);
            //}
            this.PassBackToShop(this.CashCards);
            this.PassCardsToCentre(this.CardsOnDisplay);
        }
    }

    private Card CreateMoneyCard(int amount)
    {
        if (amount > 3)
        {
            return null;
        }
        Card newCard;
        //Creates a new gameobject which based off the card in the Resource folder
        GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
        if (amount == 1)
        {
            //CARD MONEY ---------------------------
            Color cardColor = new Color(0, 0.5f, 0f, 1f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card draw mechanic to the card prefab
            CardMoneyMech cardMoneyMechanic = newCardPrefab.AddComponent<CardMoneyMech>() as CardMoneyMech;
            cardMoneyMechanic.Money = 1;
            newCard.PopulateCard("Cash", "+1 Cash", 0, cardMoneyMechanic, cardColor, "CardArt/Copper");
        }
        else if (amount == 2)
        {
            //CARD MONEY ---------------------------
            Color cardColor = new Color(0, 0.8f, 0f, 1f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card draw mechanic to the card prefab
            CardMoneyMech cardMoneyMechanic = newCardPrefab.AddComponent<CardMoneyMech>() as CardMoneyMech;
            cardMoneyMechanic.Money = 2;
            newCard.PopulateCard("Cash", "+2 Cash", 3, cardMoneyMechanic, cardColor, "CardArt/Silver");
        }
        else
        {
            //CARD MONEY ---------------------------
            Color cardColor = new Color(0, 1f, 0f, 1f);
            newCard = newCardPrefab.AddComponent<Card>() as Card;
            //attach the card draw mechanic to the card prefab
            CardMoneyMech cardMoneyMechanic = newCardPrefab.AddComponent<CardMoneyMech>() as CardMoneyMech;
            cardMoneyMechanic.Money = 3;
            newCard.PopulateCard("Cash", "+3 Cash", 6, cardMoneyMechanic, cardColor, "CardArt/Gold");
        }
        CanvasGroup cardCanvasGroup = newCard.GetComponent<CanvasGroup>();
        if (cardCanvasGroup != null)
        {
            cardCanvasGroup.alpha = 1;
            cardCanvasGroup.blocksRaycasts = true;
        }
        return newCard;
    }
}
