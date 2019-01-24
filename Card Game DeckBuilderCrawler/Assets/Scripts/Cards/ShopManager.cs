using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{

    List<Transform> CardsToBuy;
    List<Transform> CashCards;
    public bool isShopping;
    private bool isCashShopping = false;
    [SerializeField]
    private Button cashShopButton;
    [SerializeField]
    private Text cashShopButtonText;

    private void PopulateShop()
    {
        CardsToBuy = new List<Transform>();
        int numUniqueCards = 5;
        //IMPROVE SYSTEM TO ACCOUNT TO UP TO 5 CARDS IN SHOP?
        //TODO: Make the selection mechanic work for both buying cards and card mechanics e.g destroy.
        for (int i = 0; i < numUniqueCards; i++)
        {
            Card newCard;
            //Creates a new gameobject which based off the card in the Resource folder
            GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
            if (i == 0)
            {
                //DESTROY
                Color cardColor = new Color(255f, 255f, 255f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card draw mechanic to the card prefab
                CardDestroyMech cardDestroyMechanic = newCardPrefab.AddComponent<CardDestroyMech>() as CardDestroyMech;
                cardDestroyMechanic.NumCardsToDestroy = 2;
                newCard.PopulateCard("Destroy", "+2 Destroy", 2, cardDestroyMechanic, cardColor, "CardArt/Defense");
            }
            else if (i == 1)
            {
                //DRAW
                Color cardColor = new Color(250f, 69f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card draw mechanic to the card prefab
                CardDrawMech cardDrawMechanic = newCardPrefab.AddComponent<CardDrawMech>() as CardDrawMech;
                cardDrawMechanic.NumCardsToDraw = 2;
                newCard.PopulateCard("Draw", "+2 Draw", 1, cardDrawMechanic, cardColor, "CardArt/Defense");
            }
            else if (i == 2)
            {
                //MULTIPLIER
                Color cardColor = new Color(145f, 0f, 211f, 255f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card defense mechanic to the card prefab
                CardMultiplierMech cardMultiplierMechanic = newCardPrefab.AddComponent<CardMultiplierMech>() as CardMultiplierMech;
                cardMultiplierMechanic.NumTimesToMultiply = 2; ////NUm cards = mUltiply x n
                newCard.PopulateCard("Multiplier", "x2 Multiply", 2, cardMultiplierMechanic, cardColor, "CardArt/Defense", Card.CardType.Persistant);
            }
            else if (i == 3)
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
            else if (i == 4)
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
            // making the centrePanel its parent
            SelectionPanel.Instance.PassToPanel(newCard.transform, null);
            CardsToBuy.Add(newCard.transform);
        }
    }

    public void StartShop()
    {
        this.cashShopButton.gameObject.SetActive(true);
        isShopping = true;
        SelectionPanel.Instance.titleLabel.text = "Shop";
        if (this.CardsToBuy == null)
        {
            // Create the shop for the first time
            this.PopulateShop();
        }
        else
        {
            this.PassCardsToCentre(this.CardsToBuy);
        }
        foreach (Transform card in this.CardsToBuy)
        {
            CanvasGroup cardCanvasGroup = card.GetComponent<CanvasGroup>();
            if (cardCanvasGroup != null)
            {
                cardCanvasGroup.alpha = 1;
                cardCanvasGroup.blocksRaycasts = true;
            }
        }
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
            }
            DeckManager.Instance.AddCardToDeck(card);
            if (this.CardsToBuy.Contains(card))
            {
                // Remove card from the list of cards in the shop
                this.CardsToBuy.Remove(card);              
            }
        }
        this.isShopping = false;
        this.isCashShopping = false;
        this.cashShopButtonText.text = "Coin" + Environment.NewLine + "Shop";
        CardDraw.Instance.UpdateLabel();
        this.cashShopButton.gameObject.SetActive(false);


        // FAILED ATTEMPT AT CARD REPLACEMENT
        //foreach (Transform card in boughtCards)
        //{
        //    GameObject replacementCard = Instantiate(card.gameObject);
        //    replacementCard.transform.SetParent(this.transform);
        //    Card test = card.GetComponent<Card>();
        //    Card test2 = replacementCard.GetComponent<Card>();
        //    if (this.CardsToBuy.Contains(card))
        //    {
        //        //// Remove card from the list of cards in the shop
        //        //this.CardsToBuy.Remove(card);
        //        //// Create a replacement card
        //        //GameObject replacementCard = (GameObject)Instantiate(card.gameObject);
        //        //replacementCard.transform.SetParent(this.transform);
        //        //replacementCard.gameObject.GetComponent<CardActions>().isSelected = false;
        //        //replacementCard.GetComponent<CanvasGroup>().alpha = 1f;
        //        //this.CardsToBuy.Add(replacementCard.transform);
        //        //replacementCard.SetActive(false);
        //    }
        //    else if (this.CashCards.Contains(card))
        //    {
        //       // // Remove card from the list of cards in the cash shop
        //       // this.CashCards.Remove(card);
        //       // // Create a replacement card
        //       // GameObject replacementCard = (GameObject)Instantiate(card.gameObject);
        //       //// replacementCard.transform.SetParent(this.transform);
        //       // replacementCard.gameObject.GetComponent<CardActions>().isSelected = false;
        //       // replacementCard.GetComponent<CanvasGroup>().alpha = 1f;
        //       // this.CashCards.Add(replacementCard.transform);
        //       // replacementCard.SetActive(false);
        //    }
        //    //if (SelectionPanel.Instance.cardsInPanel.Contains(card.transform))
        //    //{
        //    //    SelectionPanel.Instance.cardsInPanel.Remove(card.transform);
        //    //}
        //    CardActions cardAction = replacementCard.GetComponent<CardActions>();
        //    if (cardAction != null)
        //    {
        //        cardAction.isDragable = true;
        //    }
        //    DeckManager.Instance.AddCardToDeck(replacementCard.transform);
        //}
        //this.isShopping = false;
        //CardDraw.Instance.UpdateLabel();
        //this.cashShopButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Pass the card back to the shop so it doesn't linger in the centre panel
    /// </summary>
    public void PassBackToShop(List<Transform> cards)
    {
        foreach (Transform card in cards)
        {
            //// Make the ShopManager gameObject the temp parent of the card
            //// until it needs to be displayed in the shop again
            //card.SetParent(this.transform);
            //// Ensure the card is not visible or interactable
            //CanvasGroup cardCanvasGroup = card.GetComponent<CanvasGroup>();
            //if (cardCanvasGroup != null)
            //{
            //    cardCanvasGroup.alpha = 0;
            //    cardCanvasGroup.blocksRaycasts = false;
            //}

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
            this.PassBackToShop(this.CardsToBuy);
            if (this.CashCards == null)
            {
                this.CashCards = new List<Transform>();
                int numUniqueCards = 3;
                for (int i = 0; i < numUniqueCards; i++)
                {
                    Card newCard;
                    //Creates a new gameobject which based off the card in the Resource folder
                    GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
                    if (i == 0)
                    {
                        //CARD MONEY ---------------------------
                        Color cardColor = new Color(0, 0.5f, 0f, 1f);
                        newCard = newCardPrefab.AddComponent<Card>() as Card;
                        //attach the card draw mechanic to the card prefab
                        CardMoneyMech cardMoneyMechanic = newCardPrefab.AddComponent<CardMoneyMech>() as CardMoneyMech;
                        cardMoneyMechanic.Money = 1;
                        newCard.PopulateCard("Cash", "+1 Cash", 0, cardMoneyMechanic, cardColor, "CardArt/Copper");
                    }
                    else if (i == 1)
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
            this.PassCardsToCentre(this.CardsToBuy);
        }
    }
}
