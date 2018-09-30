using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    //Current monster
    [SerializeField]
    internal MonsterInteraction monster;

    public void OnPointerEnter(PointerEventData eventData)
    {
       // if nothing is being dragged do nothing
        if (eventData.pointerDrag == null)
        {
            return;
        }
        CardActions cardAction = eventData.pointerDrag.GetComponent<CardActions>();
        cardAction.placeholderParent = this.transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //  Debug.Log("Pointer Exit");
        //if nothing is being dragged do nothing
        if (eventData.pointerDrag == null)
        {
            return;
        }
        CardActions cardAction = eventData.pointerDrag.GetComponent<CardActions>();
        if (cardAction != null && cardAction.placeholderParent == this.transform)
        {
            cardAction.placeholderParent = cardAction.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on + " + this.gameObject.name);

        CardActions cardAction = eventData.pointerDrag.GetComponent<CardActions>();
        if (cardAction != null && cardAction.isDragable)
        {
            cardAction.parentToReturnTo = this.transform;
            cardAction.isDiscarded = true;

            GameObject cardTransform = eventData.pointerDrag;
            if (cardTransform.tag == "card")
            {
                if (this.monster != null)
                {
                    //Set the current monster to the opne being interacted with
                    GameManager.Instance.currentMonster = this.monster;
                }
                Card card = eventData.pointerDrag.GetComponent<Card>();
                //This is a bad way to do this IMPROVE!! 
                if (card != null && GameManager.Instance.multiplierOn)
                {
                    //Get the orginal card value
                    int cardValue = card.iCard.GetValue();
                    //Multiply that value
                    card.iCard.SetValue(cardValue * GameManager.Instance.multiplierNum);
                    //Apply the action with the multipled value
                    card.OnApplyCardAction();
                    //Return the value to its orginal number
                    card.iCard.SetValue(cardValue);

                    GameManager.Instance.multiplierOn = false;
                    GameManager.Instance.multiplierNum = 0;
                }
                else if (card != null)
                {
                    card.OnApplyCardAction();
                }
            }
        }
    }
}
