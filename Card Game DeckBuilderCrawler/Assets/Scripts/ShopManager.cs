using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager> {

    List<Transform> CardsToBuy;
    public bool isShopping;

    private void PopulateShop()
    {
        isShopping = true;
        CardsToBuy = new List<Transform>();
        int numUniqueCards = 2;
        //IMPROVE SYSTEM TO ACCOUNT TO UP TO 5 CARDS IN SHOP?
        for (int i = 0; i < numUniqueCards; i++)
        {
            Card newCard;
            //Creates a new gameobject which based off the card in the Resource folder
            GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
            if (i < 2)
            {

                Color cardColor = new Color(250f, 69f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                CreateCard(newCard, newCardPrefab, "Destroy", "+1 Destroy", cardColor);
                //attach the card draw mechanic to the card prefab
                CardDestroyMech cardDestroyMechanic = newCardPrefab.AddComponent<CardDestroyMech>() as CardDestroyMech;
                cardDestroyMechanic.NumCardsToDestroy = 1;
                // making the centrePanel its parent
                SelectionPanel.Instance.PassToPanel(newCard.transform, cardDestroyMechanic, cardDestroyMechanic.NumCardsToDestroy);
            }
            else
            {
                Color cardColor = new Color(250f, 69f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                CreateCard(newCard, newCardPrefab, "Draw", "+2 Draw", cardColor);
                //attach the card draw mechanic to the card prefab
                CardDrawMech cardDrawMechanic = newCardPrefab.AddComponent<CardDrawMech>() as CardDrawMech;
                cardDrawMechanic.NumCardsToDraw = 2;
            }
        }
    }

    private void CreateCard(Card cardType, GameObject cardPrefab, string cardTitle, string cardDescription, Color cardColor)
    {
        cardType.PopulateCard(cardTitle, cardDescription, cardColor);
        CardsToBuy.Add(cardType.transform);
    }

    public void StartShop()
    {
        if (this.CardsToBuy == null)
        {
            this.PopulateShop();
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
}
