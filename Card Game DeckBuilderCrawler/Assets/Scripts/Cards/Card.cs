using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    private CardProperties props;
    public GameObject cardPrefab;

    public Card(string cardTitle, string cardDescription, int attack)
    {
        props = new CardProperties();
        cardPrefab = (GameObject)Instantiate(Resources.Load("Card"));   //safe way to check cast?
        cardPrefab.transform.Find("Card Title").GetComponent<Text>().text = cardTitle;
        cardPrefab.transform.Find("Card Description").GetComponent<Text>().text = cardDescription;
        props.Attack = attack;
    }
}
