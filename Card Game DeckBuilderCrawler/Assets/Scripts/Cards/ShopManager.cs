using System;
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
        //TODO: Make the selection mechanic work for both buying cards and card mechanics e.g destroy.
        for (int i = 0; i < numUniqueCards; i++)
        {
            Card newCard;
            //Creates a new gameobject which based off the card in the Resource folder
            GameObject newCardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
            if (i < 2)
            {

                Color cardColor = new Color(250f, 69f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card draw mechanic to the card prefab
                CardDestroyMech cardDestroyMechanic = newCardPrefab.AddComponent<CardDestroyMech>() as CardDestroyMech;
                cardDestroyMechanic.NumCardsToDestroy = 1;
                newCard.PopulateCard("Destroy", "+2 Destroy", cardDestroyMechanic, cardColor);
                // making the centrePanel its parent
                //SelectionPanel.Instance.PassToPanel(newCard.transform, cardDestroyMechanic, cardDestroyMechanic.NumCardsToDestroy);

            }
            else
            {
                Color cardColor = new Color(250f, 69f, 0f, 1f);
                newCard = newCardPrefab.AddComponent<Card>() as Card;
                //attach the card draw mechanic to the card prefab
                CardDrawMech cardDrawMechanic = newCardPrefab.AddComponent<CardDrawMech>() as CardDrawMech;
                cardDrawMechanic.NumCardsToDraw = 2;
                newCard.PopulateCard("Draw", "+2 Draw", cardDrawMechanic, cardColor);
                // making the centrePanel its parent
                //SelectionPanel.Instance.PassToPanel(newCard.transform, cardDrawMechanic, cardDrawMechanic.NumCardsToDraw);
            }
        }
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
