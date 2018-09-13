using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public delegate void OnApplyCardActionDelegate();
    public event OnApplyCardActionDelegate applyCardActionDelegate;

    public void PopulateCard(string cardTitle, string cardDescription, Color cardColor)
    {
        this.transform.Find("Card Title").GetComponent<Text>().text = cardTitle;
        this.transform.Find("Card Description").GetComponent<Text>().text = cardDescription;
        this.transform.GetComponent<Image>().color = cardColor;        
    }

    public void OnApplyCardAction()
    {
        applyCardActionDelegate();
    }
}
