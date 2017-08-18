using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {


    public TowerButton ClickedBtn  { get;  set; }

    private int currency;

    [SerializeField]
    private Text currencyTxt;

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {

            currency = value;
            this.currencyTxt.text = value.ToString() + " <color=yellow>G</color>";  //sets the currency with a yellow "G" after it
        }
    }

    // Use this for initialization
    void Start () {
        Currency = 5;
	}
	
	// Update is called once per frame
	void Update () {
        HandleEscape();
	}

    // called from Onclick event in unity in the Canvas -> towerPanel ->Btn
    public void PickTower(TowerButton towerBtn)
    {
        if (Currency >= towerBtn.Price) //checks you have enough money to buy tower
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }

    }

    public void BuyTower()
    {
        if (Currency >= ClickedBtn.Price) //checks you have enough money when you place the tower
        {
            Currency -= ClickedBtn.Price;   //take tower price from currency
        }
        Hover.Instance.Deavtivate();    //get tower out of hand
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deavtivate();
        }
    }
}
