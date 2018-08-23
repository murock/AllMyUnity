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
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
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
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d.placeholderParent == this.transform)
        {
            d.placeholderParent = d.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on + " + this.gameObject.name);
        GameObject card = eventData.pointerDrag;

        Draggable d = card.GetComponent<Draggable>();
        d.parentToReturnTo = this.transform;
        d.isDiscarded = false;
    }

    public void DiscardHand()
    {
        List<Draggable> cardsToDiscard = new List<Draggable>();
        //loop through all children in the hand
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform card = this.transform.GetChild(i);
            Draggable d = card.GetComponent<Draggable>();
            //If the tag is card then child is a card
            if (card.tag == "card")
            {
                cardsToDiscard.Add(d);
            }
        }
        foreach (Draggable card in cardsToDiscard)
        {
            card.parentToReturnTo = GameManager.Instance.transform;
            card.isDiscarded = true;
            card.DiscardCard();
        }
    }
}
