﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardActions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    //tells the card where it needs to go back to e.g hand,play area,deck
    public Transform parentToReturnTo = null;
    //The parent of the last place the card was
    public Transform placeholderParent = null;
    //Flag to tell if the card is discarded e.g has been played
    public bool isDiscarded = false;

    //where the card will be placed back to
    private GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Could do something here to work out difference from where its being clicked on to the anchor point of the card to avoid it jumping
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        //sets the width of the placeholder gameobject so that cards move around when you hover a card over them
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        //Puts the placeholder at the same sibling index as the card so the gap is created in the right place
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        //ensures the card will return to where it came from if dropped
        parentToReturnTo = this.transform.parent;
        //Puts the placeholder under the parent which you are dragging the card from
        placeholderParent = parentToReturnTo;
        //puts the card 1 level up e.g Canvas DANGER CODE MAY BREAK IF HIERARCHY IS CHANGED
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //allows movement of the card
        this.transform.position = eventData.position;
        //Ensure placeholder parent stays what we set it to in the begin drag
        if (placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        //New index for the placeholder as you move the card around
        int newSiblingIndex = placeholderParent.childCount;

        //loop through every card in the hand/table
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            //if this card is further left than the card we are looking at then change sibling index to match
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                Debug.Log("newSibling Index: " + newSiblingIndex.ToString() + "placeHolder sibling Index: " + placeholder.transform.GetSiblingIndex().ToString());
                //Not quite sure why this works!! Need to think about it some more with the help of the above debug line
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                {
                    newSiblingIndex--;
                }
                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.transform.SetParent(parentToReturnTo);
        if(placeholder != null)
        {
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            Destroy(placeholder); //this.DiscardCard();
        }
        
        //become targetable
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (this.isDiscarded)
        {
            //Fade out card when it goes into the play area
            //Could do an animation here instead
            //StartCoroutine(Fade());
            InstantFade();
        }


    }

    public void DiscardCard()
    {
        this.transform.SetParent(parentToReturnTo);
        if (placeholder != null)
        {
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            Destroy(placeholder);
        }
        if (this.isDiscarded)
        {
            //Fade out card when it goes into the play area
            //Could do an animation here instead
            // StartCoroutine(Fade());
            InstantFade();
        }
    }

    public void ShuffleCardBack()
    {
        this.transform.SetParent(parentToReturnTo);
        if (placeholder != null)
        {
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            Destroy(placeholder);
        }
        this.isDiscarded = false;
    }

    IEnumerator Fade()
    {
        float time = 0f;
        float duration = 3f;
        CanvasGroup cardCanvasGroup = this.GetComponent<CanvasGroup>();
        //become untagetable
        cardCanvasGroup.blocksRaycasts = false;
        while (time < duration)//(cardCanvasGroup.alpha != 0f)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp01(time / duration);
            cardCanvasGroup.alpha = Mathf.Lerp(1, 0, blend);
            //cardCanvasGroup.alpha = Mathf.Lerp(1,0,time * 0.0001f);
            //time += Time.deltaTime;
            Debug.Log("in while loop");
            // yield return null;
            yield return null;
        }
    }

    //Put this in as bug with Fade() race condition of wanting alpha to be 1 but fade setting it to 0 when ending turn and wanting cards there were just in the hand
    private void InstantFade()
    {
        CanvasGroup cardCanvasGroup = this.GetComponent<CanvasGroup>();
        //become untagetable
        cardCanvasGroup.blocksRaycasts = false;
        cardCanvasGroup.alpha = 0;
    }

}
