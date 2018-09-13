using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionPanel : Singleton<SelectionPanel>
{
    List<Transform> cardsInPanel = new List<Transform>();
    ICardSelectableMech currentMech;
    public void Accept()
    {
        List<Transform> cardsSelected = new List<Transform>();
        List<Transform> cardsNotSelected = new List<Transform>();
        //CHANGE TO LOOP THROUGH CARDS IN PANEL
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform card = this.transform.GetChild(i);
            //If the tag is card then child is a card
            if (card.tag == "card")
            {
                CardActions cardAction = card.GetComponent<CardActions>();
                if (cardAction.isSelected)
                {
                    //Card is selected
                    cardsSelected.Add(card);
                }
                else
                {
                    //Card not selected
                    cardsNotSelected.Add(card);
                }
            }
        }

        //Apply Selection Action
        currentMech.ApplySelectionAction(cardsSelected, cardsNotSelected);
        this.gameObject.SetActive(false);
    }

    public void PassToPanel(Transform card, ICardSelectableMech cardMech)
    {
        card.SetParent(this.transform);
        cardsInPanel.Add(card);
        currentMech = cardMech;
    }
}
