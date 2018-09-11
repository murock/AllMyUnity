using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : Singleton<Hand>, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if nothing is being dragged do nothing
        if (eventData.pointerDrag == null)
        {
            return;
        }
        //Make the cards placeholder parent the location you are dragging the card to
        CardActions cardAction = eventData.pointerDrag.GetComponent<CardActions>();
        cardAction.placeholderParent = this.transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if nothing is being dragged do nothing
        if (eventData.pointerDrag == null)
        {
            return;
        }
        //Make the cards placeholder parent the previous location it was in
        CardActions d = eventData.pointerDrag.GetComponent<CardActions>();
        if (d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        //make the parent of the card the hand when dropped into the hand
        GameObject card = eventData.pointerDrag;
        CardActions cardAction = card.GetComponent<CardActions>();
        if (cardAction != null)
        {
            cardAction.parentToReturnTo = this.transform;
            cardAction.isDiscarded = false;
        }
    }

    public void DiscardHand()
    {
        List<CardActions> cardsToDiscard = new List<CardActions>();
        //loop through all children in the hand
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform card = this.transform.GetChild(i);
            //If the tag is card then child is a card
            if (card.tag == "card")
            {
                CardActions cardAction = card.GetComponent<CardActions>();
                cardsToDiscard.Add(cardAction);
            }
        }
        foreach (CardActions card in cardsToDiscard)
        {
            card.parentToReturnTo = GameManager.Instance.transform;
            card.isDiscarded = true;
            card.DiscardCard();
        }
    }
}
