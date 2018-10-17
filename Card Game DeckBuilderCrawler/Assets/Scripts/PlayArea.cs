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
            Card card = eventData.pointerDrag.GetComponent<Card>();      
            if (card.cardCardType != Card.CardType.Persistant)
            {
                // If its a persistant card don't discard
                cardAction.isDiscarded = true;
                // Ensure the card goes to the playarea its been put on
                cardAction.parentToReturnTo = this.transform;
            }
            else
            {
                PersistantPanel persistantPanel = this.GetComponent<PersistantPanel>();
                // if this playarea is attached to a persistant panel
                if (persistantPanel != null)
                {
                    // If passing to the persistant panel was successful
                    if (persistantPanel.PassToPersistantPanel(card.transform))
                    {
                        // Ensure the card goes to the peristant panel;
                        cardAction.parentToReturnTo = persistantPanel.GetComponent<Transform>();
                    }
                }
                
            }

            GameObject cardTransform = eventData.pointerDrag;
            if (cardTransform.tag == "card")
            {
                if (this.monster != null)
                {
                    //Set the current monster to the one being interacted with i.e the one attached to this PlayArea
                    GameManager.Instance.currentMonster = this.monster;
                }
                //This is a bad way to do this IMPROVE!! 
                if (card != null && GameManager.Instance.multiplierOn)
                {
                    //Get the orginal card value
                    //int cardValue = card.iCard.GetValue();
                    //Multiply that value
                    //card.iCard.SetValue(cardValue * GameManager.Instance.multiplierNum);
                   // card.SetMechValue(cardValue * GameManager.Instance.multiplierNum);
                    //Apply the action with the multipled value
                   // card.OnApplyCardAction();
                    //Return the value to its orginal number
                   // card.iCard.SetValue(cardValue);
                    //card.SetMechValue(cardValue);

                    GameManager.Instance.multiplierOn = false;
                    GameManager.Instance.multiplierNum = 0;
                }
                else if (card != null)
                {
                    if (GameManager.Instance.persistantCardArea.IsPopulated)
                    {
                        GameManager.Instance.persistantCardArea.ApplyCardPersistantAction(card);
                    }
                    card.OnApplyCardAction();
                }
            }
        }
    }
}
