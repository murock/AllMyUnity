using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;
    public bool isDiscarded = false;

    //where the card will be placed back to
    private GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Could do something here to work out difference from where its being clicked on to the anchor point of the card to avoid it jumping
        Debug.Log("OnBeginDrag");

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeholderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        //allows movement of the card
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
        {
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount;

        //loop through every card in the hand/table
        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            //if this card is further left than the card we are looking at then change sibling index to match
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
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
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        //become targetable
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);
        if (this.isDiscarded)
        {
            //Fade out card when it goes into the play area
            //Could do an animation here instead
            StartCoroutine(Fade());
        }
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

}
