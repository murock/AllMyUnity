using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

  //  private CardProperties props;
    public GameObject cardPrefab;

    public Card(string cardTitle, string cardDescription, int attack, int defense, Color cardColor)
    {
        // props = new CardProperties();
        cardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
        cardPrefab.transform.Find("Card Title").GetComponent<Text>().text = cardTitle;
        cardPrefab.transform.Find("Card Description").GetComponent<Text>().text = cardDescription;
        cardPrefab.transform.GetComponent<Image>().color = cardColor;
        CardProperties props = cardPrefab.transform.GetComponent<CardProperties>();
        props.Attack = attack;
        props.Defense = defense;
    }
}
