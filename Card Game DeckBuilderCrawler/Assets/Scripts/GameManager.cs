using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    private Text moneyText;
    public MonsterInteraction monster;
    public Hand hand;
    public GameObject centrePanel;
    public bool multilyOn;
    public GameObject player;

    private int money;

    //To be implemented still for efficency
    public ObjectPool Pool { get; set; }

    //This is called before Start()
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        TurnManager.Instance.StartCoroutine(TurnManager.Instance.DrawHand());
    }

    public void AdjustMoney(int amount)
    {
        this.money += amount;
        this.moneyText.text = "Cash Money: <color=green>$" + this.money.ToString() + "</color>";
    }

}
