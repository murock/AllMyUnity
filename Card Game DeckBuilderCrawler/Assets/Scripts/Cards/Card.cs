using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public delegate void OnApplyCardActionDelegate();
    public event OnApplyCardActionDelegate applyCardActionDelegate;
    public ICardMech iCard;
    public List<ICardMech> iCards;
    private int cost;
    private CardType cardType;

    public enum CardType
    {
        Normal,
        Persistant
    }

    public int Cost
    {
        get
        {
            return this.cost;
        }
    }

    public CardType cardCardType
    {
        get
        {
            return this.cardType;
        }
    }

    public void PopulateCard(string cardTitle, string cardDescription, int cardCost, ICardMech iCard, Color cardColor, string artLocation, CardType cardType = CardType.Normal)
    {
        this.cardType = cardType;
        this.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(artLocation);
        this.transform.Find("Card Title").GetComponent<Text>().text = cardTitle;
        this.transform.Find("Card Description").GetComponent<Text>().text = cardDescription;
        this.transform.Find("Card Cost").GetComponent<Text>().text = "<color=green>$</color>" + cardCost.ToString();
        this.cost = cardCost;
        this.transform.GetComponent<Image>().color = cardColor;
        //this.iCard = iCard;
        if (this.iCards == null)
        {
            //If not yet initialized than initialize
            this.iCards = new List<ICardMech>();
        }
        this.iCards.Add(iCard);
    }

    public void AddAddtionalMech(ICardMech iCard)
    {
        this.iCards.Add(iCard);
    }

    //Changes the value of the mech e.g draw X cards
    public void SetMechValue(int multiplier)
    {
        foreach (ICardMech iCard in this.iCards)
        {
            int orginalValue = iCard.GetValue();
            iCard.SetValue(orginalValue*multiplier);
        }
    }

    public void OnApplyCardAction()
    {
         applyCardActionDelegate();           
    }

    
}
