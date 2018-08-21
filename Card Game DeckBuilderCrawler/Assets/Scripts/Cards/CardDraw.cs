using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDraw : Singleton<CardDraw>{

    [SerializeField]
    private GameObject deckLabel;

    [SerializeField]
    private GameObject hand;

    public List<GameObject> deck;


    public void drawCard()
    {
        //cards still left to draw
        if (deck.Count > 0)
        {
            
            int randomNumber = Random.Range(0, deck.Count - 1);
            //Make the hand the parent of the card
            deck[randomNumber].transform.parent = hand.transform;
            //Make visible and clickable
            CanvasGroup cardCanvasGroup = deck[randomNumber].GetComponent<CanvasGroup>();
            cardCanvasGroup.alpha = 1;
            cardCanvasGroup.blocksRaycasts = true;

            //remove card from deck list
            deck.RemoveAt(randomNumber);
        }
        if (deck.Count > 0)
        {
            deckLabel.GetComponent<Text>().text = "Deck" + System.Environment.NewLine + "Cards Left: " + deck.Count.ToString();
        }
        else
        {
            deckLabel.GetComponent<Text>().text = "Deck" + System.Environment.NewLine +  "Out of Cards";
        }

    }
}
