﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SelectionPanel : Singleton<SelectionPanel>
{
    [SerializeField]
    public Text titleLabel;
    [SerializeField]
    private Button acceptButton;

    Queue<Transform> cardsSelectedQueue = new Queue<Transform>();
    List<Transform> cardsNotSelected = new List<Transform>();
    List<Transform> cardsInPanel = new List<Transform>();
    ICardSelectableMech currentMech;
    //The maximum number of cards you can select
    int maxSelectable = 0;
    private int costOfSelectedCards;

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
            //Remove specific item from Q
            cardsSelectedQueue = new Queue<Transform>(cardsSelectedQueue.Where(t => t != card.transform));
            cardAction.isSelected = false;
            cardsNotSelected.Add(card.transform);
            cardAction.HighlightCard();
            CheckMoney();
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
                cardAction.HighlightSelectedCard();
            }
            if (cardsNotSelected.Contains(card.transform))
            {
                cardsNotSelected.Remove(card.transform);
            }
        }
        CheckMoney();
    }

    private void CheckMoney()
    {
        //If shopping and too much value of cards selected display warning message and disabled accept button
        if (ShopManager.Instance.isShopping)
        {
            int costOfCards = 0;
            foreach (Transform cardTransform in this.cardsSelectedQueue)
            {
                Card cardComp = cardTransform.GetComponent<Card>();
                if (cardComp != null)
                {
                    costOfCards += cardComp.Cost;
                }
            }
            this.costOfSelectedCards = costOfCards;
            if (costOfCards <= GameManager.Instance.Money)
            {
                this.titleLabel.text = "Shop";
                this.acceptButton.interactable = true;
            }
            else
            {
                //If you don't have enough money then do no action
                this.titleLabel.text = "Not enough cash";
                this.acceptButton.interactable = false;
            }
        }
    }

    public void Accept()
    {
        //Do shop action if shopping
        if (ShopManager.Instance.isShopping)
        {
            GameManager.Instance.AdjustMoney(-this.costOfSelectedCards);
            //Send card(s) to buy and not to buy to the shop manager
            ShopManager.Instance.BuyCards(this.cardsSelectedQueue.ToList());
        }
        //Apply Selection Action
        else if (currentMech != null)
        {
            currentMech.ApplySelectionAction(this.cardsSelectedQueue.ToList(), this.cardsNotSelected);
        }

        //Empty the lists when finished
        this.cardsInPanel.Clear();
        this.cardsNotSelected.Clear();
        this.cardsSelectedQueue.Clear();
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
