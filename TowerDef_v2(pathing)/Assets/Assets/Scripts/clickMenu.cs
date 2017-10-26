using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMenu : Singleton<ClickMenu>
{
    [SerializeField]
    private Canvas myCanvas;

    [SerializeField]
    private GameObject menu;

    //private GameObject cacheMenu;

    // Use this for initialization
    void Start()
    {
     //   this.menu.SetActive(false);
      //  this.myCanvas = GetComponentInParent<Canvas>(); //gets the main canvas

    }

    private void Awake()
    {
        //this.cacheMenu = menu;
        //this.menu = GetComponent<GameObject>();
        this.menu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
      //  SpawnMenu();
    }

    public void SpawnMenu(Vector3 spawnPosition)
    {
      //  this.menu = cacheMenu;
        this.menu.SetActive(true);
            //The below 3 lines allow the menu to appear where the mouse is on click
            // more info found http://answers.unity3d.com/questions/849117/46-ui-image-follow-mouse-position.html
            Vector2 pos;
              //  RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, spawnPosition, myCanvas.worldCamera, out pos);
            this.menu.transform.position = myCanvas.transform.TransformPoint(pos);

    }

    public void HideMenu()
    {
        this.menu.SetActive(false);
    }

}
