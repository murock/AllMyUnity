using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {

    private List<GameObject> deck;
    private int deckSize;

    // Use this for initialization
    void Awake () {
        deck = new List<GameObject>();
        deckSize = 10;
        for (int i = 0; i < deckSize; i++)
        {
            Card newCard;
            if (i < 3)
            {
                Color cardColor = new Color(1f, 0f, 0f, 1f);
                newCard = new Card("Attack", "+1 attack", 1, 0, cardColor);
                
            }
            else
            {
                Color cardColor = new Color(0f, 0f, 1f, 1f);
                newCard = new Card("Defense", "+1 Defense", 0, 1, cardColor);
            }

            newCard.cardPrefab.transform.parent = this.transform; // making the deck its parent
            CanvasGroup cardCanvasGroup = newCard.cardPrefab.GetComponent<CanvasGroup>();
            cardCanvasGroup.alpha = 0; //making it not visible
            cardCanvasGroup.blocksRaycasts = false;
            deck.Add(newCard.cardPrefab);

        }
        CardDraw.Instance.deck = this.deck;

	}

	
}
