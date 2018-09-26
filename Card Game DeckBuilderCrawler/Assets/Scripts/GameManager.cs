﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

    [SerializeField]
    public MonsterInteraction monster;
    public Hand hand;
    public GameObject centrePanel;
    public bool multilyOn;
    public Text moneyText;

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

   
}
