using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMenu : Singleton<ClickMenu>
{
    //if you want to disable double click https://msdn.microsoft.com/en-us/library/windows/desktop/ms645606(v=vs.85).aspx
    [SerializeField]
    private Canvas myCanvas;

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private Button[] TwrBtns;
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

    public void CycleButtons()
    {
        //Vector3 pos = Twr1Btn.transform.position;       
        //Vector3 pos2 = Twr2Btn.transform.position;
        //Twr1Btn.transform.position = pos2;
        //Twr2Btn.transform.position = pos;
        for (int i = TwrBtns.Length - 1; i > 0; i--)
        {
            Vector3 pos = TwrBtns[i].transform.position;
            Vector3 pos2 = TwrBtns[i-1].transform.position;
            TwrBtns[i].transform.position = pos2;
            TwrBtns[i-1].transform.position = pos;
        }

    }
}
