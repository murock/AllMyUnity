using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public delegate void OnApplyCardActionDelegate();
    public event OnApplyCardActionDelegate applyCardActionDelegate;
    public ICardMech iCard;

    public void PopulateCard(string cardTitle, string cardDescription, ICardMech iCard, Color cardColor)
    {
        this.transform.Find("Card Title").GetComponent<Text>().text = cardTitle;
        this.transform.Find("Card Description").GetComponent<Text>().text = cardDescription;
        this.transform.GetComponent<Image>().color = cardColor;
        this.iCard = iCard;
    }

    public void OnApplyCardAction()
    {

         applyCardActionDelegate();  
          
    }

    
}
