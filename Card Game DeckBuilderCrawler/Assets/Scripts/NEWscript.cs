using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWscript : MonoBehaviour {

    public void ChooseCards()
    {

        if (PlayArea.Instance.monster.Health == 0)
        {
            Card CardChoice1;

            Color cardColor = new Color(2f, 0f, 0f, 1f);
            CardChoice1 = new Card("Attack", "+2 attack", 2, 0, 0, cardColor);


            CardChoice1.cardPrefab.transform.SetParent(DeckManager.Instance.transform);
            //CardChoice1.cardPrefab.transform.parent = DeckManager.Instance.deck.transform; // making the deck its parent
            CanvasGroup cardCanvasGroup = CardChoice1.cardPrefab.GetComponent<CanvasGroup>();
            cardCanvasGroup.alpha = 0;
            cardCanvasGroup.blocksRaycasts = false;
            DeckManager.Instance.deck.Add(CardChoice1.cardPrefab.transform);

        }
    }


}
