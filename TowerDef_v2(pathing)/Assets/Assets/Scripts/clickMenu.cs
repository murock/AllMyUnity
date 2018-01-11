using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMenu : Singleton<ClickMenu>
{
    [SerializeField]
    private Canvas myCanvas;

    [SerializeField]
    private GameObject menu;

    // Use this for initialization
    private void Awake()
    {
        this.menu.SetActive(false);
    }

    public void SpawnMenu(Vector3 spawnPosition)
    {
        this.menu.SetActive(true);
        //Hover.Instance.Deavtivate(); // need to get into the tower individual range Tower.Select will toggle the menu
        GameManager.Instance.DeselectTower();
            //The below 3 lines allow the menu to appear where the mouse is on click
            // more info found http://answers.unity3d.com/questions/849117/46-ui-image-follow-mouse-position.html
        Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, spawnPosition, myCanvas.worldCamera, out pos);
            this.menu.transform.position = myCanvas.transform.TransformPoint(pos);

    }

    public void HideMenu()
    {
        this.menu.SetActive(false);
    }

}
