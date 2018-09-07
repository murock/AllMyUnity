using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : Singleton<Hand>, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter(PointerEventData eventData)
    {
        //  Debug.Log("Pointer Enter");
        //if nothing is being dragged do nothing
        if (eventData.pointerDrag == null)
        {
            return;
        }
        CardActions d = eventData.pointerDrag.GetComponent<CardActions>();
        d.placeholderParent = this.transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //  Debug.Log("Pointer Exit");
        //if nothing is being dragged do nothing
        if (eventData.pointerDrag == null)
        {
            return;
        }
        CardActions d = eventData.pointerDrag.GetComponent<CardActions>();
        if (d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on + " + this.gameObject.name);
        GameObject card = eventData.pointerDrag;

        CardActions d = card.GetComponent<CardActions>();
        d.parentToReturnTo = this.transform;
        d.isDiscarded = false;
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
                CardActions d = card.GetComponent<CardActions>();
                cardsToDiscard.Add(d);
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
