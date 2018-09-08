using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public delegate void OnApplyCardActionDelegate();
    public event OnApplyCardActionDelegate applyCardActionDelegate;
    public void PopulateCard(string cardTitle, string cardDescription, int defense, Color cardColor)
    {
        this.transform.Find("Card Title").GetComponent<Text>().text = cardTitle;
        this.transform.Find("Card Description").GetComponent<Text>().text = cardDescription;
        this.transform.GetComponent<Image>().color = cardColor;
        CardProperties props = this.transform.GetComponent<CardProperties>();
        props.Defense = defense;
    }

    public void OnApplyCardAction()
    {
        applyCardActionDelegate();
    }
}
