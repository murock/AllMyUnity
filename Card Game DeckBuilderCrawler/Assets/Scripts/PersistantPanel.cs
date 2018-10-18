using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantPanel : MonoBehaviour {

    private bool isPopulated;
    IPersistantCard persistantCard;

    public bool IsPopulated {
        get
        {
            return isPopulated;
        }
    }

    public bool PassToPersistantPanel(Transform transform)
    {
        //If panel does not already have a transform in it then add this one
        if (!this.isPopulated)
        {
            //card becomes untargetable, THIS IS BEING PUT BACK TO TRYE IN ONENDDRAG IN CardActions.cs
            //CanvasGroup canvasGroup = transform.GetComponent<CanvasGroup>();
            //if (canvasGroup != null)
            //{
            //    canvasGroup.blocksRaycasts = false;
            //}
            transform.SetParent(this.transform);
            persistantCard = transform.GetComponent<IPersistantCard>();
            this.isPopulated = true;
            return true;
        }
        return false;
    }

    public void RemoveFromPanel(Transform transform)
    {
        if (transform.IsChildOf(this.transform))
        {
            CardActions cardAction = transform.GetComponent<CardActions>();
            if (cardAction != null)
            {
                cardAction.isDiscarded = true;
                cardAction.DiscardCard();
                this.isPopulated = false;
            }
        }

    }

    public void ApplyCardPersistantAction(Card card)
    {
        if (persistantCard != null)
        {
            persistantCard.ApplyPersistAction(card);
        }
    }
}
