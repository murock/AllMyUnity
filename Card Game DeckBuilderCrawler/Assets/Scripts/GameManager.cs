using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    
    [SerializeField]
    private Text moneyText;
    public MonsterInteraction currentMonster;
    public Hand hand;
    public GameObject centrePanel;
    public GameObject player;
    public PersistantPanel persistantCardArea;
    public bool multiplierOn;
    public int multiplierNum;
    public Text toolTip;
    public CanvasGroup toolTipCanvasGroup;

    private int money;

    public int Money
    {
        get
        {
            return this.money;
        }
        set
        {
            this.AdjustMoney(value);
        }
    }


    //To be implemented still for efficency
    public ObjectPool Pool { get; set; }

    //This is called before Start()
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        StartCoroutine(CardDraw.Instance.DrawCards());
    }

    public void AdjustMoney(int amount)
    {
        this.money += amount;
        this.moneyText.text = "<color=green>$</color>" + this.money.ToString();
    }

}
