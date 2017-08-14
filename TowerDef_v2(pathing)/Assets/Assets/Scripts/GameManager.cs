using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {


    public TowerButton ClickedBtn  { get; private set; }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // called from Onclick event in unity in the Canvas -> towerPanel ->Btn
    public void PickTower(TowerButton towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
    }

    public void BuyTower()
    {
        this.ClickedBtn = null;
    }
}
