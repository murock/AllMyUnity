using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {


    public TowerButton ClickedBtn  { get;  set; }

    private int currency;

    [SerializeField]
    private Text currencyTxt;

    public ObjectPool Pool { get; set; }

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

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Use this for initialization
    void Start () {
        Currency = 200;
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

    public void StartWave()
    {
        StartCoroutine(SpawnWave());    // coroutine so they spawn gradually
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();

        int monsterIndex = 4;//Random.Range(0, 4);

        string type = string.Empty;

        switch (monsterIndex)   //spawn a monster based on the random number
        {
            case 0:
                type = "Seedo";
                break;
            case 1:
                type = "Shroom";
                break;
            case 2:
                type = "Snail";
                break;
            case 3:
                type = "Tree";
                break;
            case 4:
                type = "Girl";
                break;
            default:
                break;
        }

        Monster monster = Pool.GetObject(type).GetComponent<Monster>();
        monster.Spawn();
        yield return new WaitForSeconds(2.5f);
    }
}
