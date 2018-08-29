using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDraw : Singleton<CardDraw>{

    [SerializeField]
    private GameObject deckLabel;

    [SerializeField]
    private GameObject hand;

    public List<Transform> deck;


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
            //Put cards back into the deck
            //for (int i = 0; i < GameManager.Instance.transform.childCount; i++)
            //{
            //    Transform card = GameManager.Instance.transform.GetChild(i);
            //    reShuffleCard(card);
            //}
            //for (int i = 0; i < PlayArea.Instance.transform.childCount; i++)
            //{
            //    Transform card = PlayArea.Instance.transform.GetChild(i);
            //    reShuffleCard(card);
            //}
        }

    }

    private void reShuffleCard(Transform card)
    {
        //If the tag is card then is a card
        if (card.tag == "card")
        {
            deck.Add(card);
            Draggable d = card.GetComponent<Draggable>();
            
            //CanvasGroup cardCanvasGroup = card.GetComponent<CanvasGroup>();
            ////visible
            //cardCanvasGroup.alpha = 1;
            ////interactable
            //cardCanvasGroup.blocksRaycasts = true;
            d.parentToReturnTo = this.transform;
          //  d.isDiscarded = false;
            d.ShuffleCardBack();
        }
    }
}
