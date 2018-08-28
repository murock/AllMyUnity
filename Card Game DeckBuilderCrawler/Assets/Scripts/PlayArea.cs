using System.Collections;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        //  Debug.Log("Pointer Enter");
       // if nothing is being dragged do nothing
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
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        d.parentToReturnTo = this.transform;
        d.isDiscarded = true;

        //Card action logic

        if (card.tag == "card")
        {
            CardProperties cardProps = card.GetComponent<CardProperties>();
            //Attack
            if (cardProps.Attack > 0)
            {
                //apply damage to monster
                monster.TakeDamage(cardProps.Attack);
            }
            if (cardProps.Defense > 0)
            {
                //add defence to player
                player.AddDefence(cardProps.Defense);
            }
            if (cardProps.Drawcard > 0)
            {
                // Draw cards
                for (int i = 0; i < cardProps.Drawcard; i++)
                {
                    CardDraw.Instance.drawCard();
                }
            }
        }
    }

}
