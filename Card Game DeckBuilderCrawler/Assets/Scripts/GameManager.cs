using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public ObjectPool Pool { get; set; }
    private TurnManager turnManager;

    //This is called before Start()
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        turnManager = new TurnManager();
        StartCoroutine(turnManager.DrawInitialHand());
    }

    public void MakeCardTEST()
    {
       // Card newCard = new Card("Attack", "+1 Attack", 1);
        // newCard.cardPrefab.transform.parent = this.transform;
    }
}
