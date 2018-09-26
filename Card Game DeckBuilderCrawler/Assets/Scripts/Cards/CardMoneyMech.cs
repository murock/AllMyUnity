using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMoneyMech : MonoBehaviour, ICardMech
{
    //The amount of money the card will give you
    private int money;
    //the card which this attack mechanic is attached to
    private Card card;

    int ICardMech.GetValue()
    {
        throw new System.NotImplementedException();
    }

    void ICardMech.SetValue(int value)
    {
        throw new System.NotImplementedException();
    }
    public int Money
    {
        get
        {
            return this.money;
        }
        set
        {
            this.money = value;
            this.card = this.GetComponent<Card>();
            //attach the apply attack function to the apply card action event
            this.card.applyCardActionDelegate += ApplyMoney;
        }
    }

    private void ApplyMoney()
    {
        //string currentMoneyString = GameManager.Instance.moneyText.text;
        ////Get just the number value after the $ sign
        //currentMoneyString = currentMoneyString.Substring(currentMoneyString.LastIndexOf('$') + 1);
        //int currentMoney;
        //if (!Int32.TryParse(currentMoneyString, out currentMoney))
        //{
        //    //if the conversion from string to int fails then reset the amount
        //    currentMoney = 0;
        //}
        ////Add the money from the card
        //currentMoney += this.money;
        //GameManager.Instance.moneyText.text = "Cash Money: $" + currentMoney.ToString();
        GameManager.Instance.AdjustMoney(this.money);
    }
}
