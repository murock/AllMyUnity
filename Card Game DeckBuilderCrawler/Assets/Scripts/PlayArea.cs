﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayArea : Singleton<PlayArea>, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    //Current monster
    [SerializeField]
    internal MonsterInteraction monster;

    [SerializeField]
    private PlayerInteraction player;

    public delegate void OnApplyCardActionDelegate();
    public static event OnApplyCardActionDelegate applyCardActionDelegate;

    public void OnPointerEnter(PointerEventData eventData)
    {
       // if nothing is being dragged do nothing
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

        CardActions cardAction = eventData.pointerDrag.GetComponent<CardActions>();
        if (cardAction != null)
        {
            cardAction.parentToReturnTo = this.transform;
            cardAction.isDiscarded = true;
        }
        GameObject cardTransform = eventData.pointerDrag;
        if (cardTransform.tag == "card")
        {
            //MECHANICS ARE CLASSES
            //Triggers the card action event
            // OnApplyCardAction();
            Card card = eventData.pointerDrag.GetComponent<Card>();
            if (card != null)
            {
                card.OnApplyCardAction();
            }


            //CARD INHERITANCE
            //Card card = eventData.pointerDrag.GetComponent<Card>();
            //if (card != null)
            //{
            //    card.ApplyAction();
            //}

            //ORGINAL
            //CardProperties cardProps = card.GetComponent<CardProperties>();
            ////Attack
            //if (cardProps.Attack > 0)
            //{
            //    //apply damage to monster
            //    monster.TakeDamage(cardProps.Attack);
            //}
            //if (cardProps.Defense > 0)
            //{
            //    //add defence to player
            //    player.AddDefence(cardProps.Defense);
            //}
            //if (cardProps.Drawcard > 0)
            //{
            //    // Draw cards
            //    for (int i = 0; i < cardProps.Drawcard; i++)
            //    {
            //        CardDraw.Instance.drawCard();
            //    }
            //}
        }
    }

    public void OnApplyCardAction()
    {
        applyCardActionDelegate();
    }

}
