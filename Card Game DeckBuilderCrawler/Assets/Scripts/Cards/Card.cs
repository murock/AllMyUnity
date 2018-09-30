using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public delegate void OnApplyCardActionDelegate();
    public event OnApplyCardActionDelegate applyCardActionDelegate;
    public ICardMech iCard;
    private int cost;

    public int Cost
    {
        get
        {
            return this.cost;
        }
    }

    public void PopulateCard(string cardTitle, string cardDescription, int cardCost, ICardMech iCard, Color cardColor)
    {
        this.transform.Find("Card Title").GetComponent<Text>().text = cardTitle;
        this.transform.Find("Card Description").GetComponent<Text>().text = cardDescription;
        this.transform.Find("Card Cost").GetComponent<Text>().text = "<color=green>$</color>" + cardCost.ToString();
        this.cost = cardCost;
        this.transform.GetComponent<Image>().color = cardColor;
        this.iCard = iCard;
    }

    public void OnApplyCardAction()
    {
         applyCardActionDelegate();           
    }

    
}
