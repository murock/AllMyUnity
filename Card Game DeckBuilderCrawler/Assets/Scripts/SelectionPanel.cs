using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectionPanel : Singleton<SelectionPanel>
{
    Queue<Transform> cardsSelectedQueue = new Queue<Transform>();
    List<Transform> cardsNotSelected = new List<Transform>();
   // Queue<Transform> cardsInPanelQueue = new Queue<Transform>(); 
    List<Transform> cardsInPanel = new List<Transform>();
    ICardSelectableMech currentMech;
    //The maximum number of cards you can select
    int maxSelectable = 0;
    int currentAmtSelected = 0;

    public int CurrentAmtSelected
    {
        get
        {
            int currentSelected = 0;
            foreach (Transform card in this.cardsInPanel)
            //foreach (Transform card in this.cardsInPanelQueue)
            {
                CardActions cardAction = card.GetComponent<CardActions>();
                if (cardAction != null && cardAction.isSelected)
                {
                    currentSelected++;
                }
            }
            return currentSelected;
        }
    }

    public int MaxSelectable
    {
        get
        {
            return this.maxSelectable;
        }
    }

    public void SelectCard(GameObject card)
    {
        CardActions cardAction = card.gameObject.GetComponent<CardActions>();
        //If already selected just unselect
        if (cardAction.isSelected)
        {
            //check Q length???????
            cardsSelectedQueue.Dequeue();
            cardAction.isSelected = false;
            cardsNotSelected.Add(card.transform);
            //DO NOT HARD CODE LIKE THIS TODO: remove later
            card.GetComponent<CanvasGroup>().alpha = 0.6f;
            return;
        }
        //If maxed out on selections already unSelected the first one that was selected??
        if (this.CurrentAmtSelected >= maxSelectable)
        {
            Transform removedCard = cardsSelectedQueue.Dequeue();
            CardActions removedCardAction = removedCard.gameObject.GetComponent<CardActions>();
            removedCardAction.isSelected = false;
            cardsNotSelected.Add(removedCard);

            //unhighlight unselected card
            removedCard.GetComponent<CanvasGroup>().alpha = 1f;
        }
        //Add Selected card
        if (this.maxSelectable > 0)
        {
            if (!cardsSelectedQueue.Contains(card.transform))
            {
                cardAction.isSelected = true;
                cardsSelectedQueue.Enqueue(card.transform);
                card.GetComponent<CanvasGroup>().alpha = 0.3f;
            }
            if (cardsNotSelected.Contains(card.transform))
            {
                cardsNotSelected.Remove(card.transform);
            }
        }
    }

    public void Accept()
    {
       // List<Transform> cardsSelected = new List<Transform>();
       // List<Transform> cardsNotSelected = new List<Transform>();
       // foreach (Transform card in this.cardsInPanel)
       //// foreach (Transform card in this.cardsInPanelQueue)
       // {
       //     CardActions cardAction = card.GetComponent<CardActions>();
       //     if (cardAction != null && cardAction.isSelected)
       //     {
       //         //Card is selected
       //         cardsSelected.Add(card);
       //     }
       //     else
       //     {
       //         //Card not selected
       //         cardsNotSelected.Add(card);
       //     }
       // }

        //Apply Selection Action
        if (currentMech != null)
        {
            currentMech.ApplySelectionAction(this.cardsSelectedQueue.ToList(), this.cardsNotSelected);
        }
        //Do shop action if shopping
        if (ShopManager.Instance.isShopping)
        {
            //Send card(s) to buy and not to buy to the shop manager
        }


        this.gameObject.SetActive(false);
    }

    public void PassToPanel(Transform card, ICardSelectableMech cardMech, int maxToSelect)
    {
        this.maxSelectable = maxToSelect;
        CardActions cardAction = card.GetComponent<CardActions>();
        if (cardAction != null)
        {
            cardAction.isDragable = false;
        }

        card.SetParent(this.transform);
        cardsInPanel.Add(card);
        cardsNotSelected.Add(card);
        currentMech = cardMech;
    }
}
