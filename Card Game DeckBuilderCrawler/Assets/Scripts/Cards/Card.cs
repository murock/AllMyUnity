using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public void PopulateCard(string cardTitle, string cardDescription, int attack, int defense, int cardDraw, Color cardColor)
    {
        // props = new CardProperties();
        this.transform.Find("Card Title").GetComponent<Text>().text = cardTitle;
        this.transform.Find("Card Description").GetComponent<Text>().text = cardDescription;
        this.transform.GetComponent<Image>().color = cardColor;
        CardProperties props = this.transform.GetComponent<CardProperties>();
        props.Attack = attack;
        props.Defense = defense;
        props.Drawcard = cardDraw;
    }
}
