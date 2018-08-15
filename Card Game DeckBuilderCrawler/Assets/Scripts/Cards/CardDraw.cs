using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDraw : MonoBehaviour{

    [SerializeField]
    private GameObject deckLabel;

    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private List<GameObject> initalDeck;
    private List<GameObject> deck;


    private void Start()
    {
        deck = initalDeck;
    }
    public void drawCard()
    {
        //cards still left to draw
        if (deck.Count > 0)
        {
            deckLabel.GetComponent<Text>().text = "Deck" + System.Environment.NewLine + "Cards Left: " + deck.Count.ToString();
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
        else
        {
            deckLabel.GetComponent<Text>().text = "Deck" + System.Environment.NewLine +  "Out of Cards";
        }

    }
}
