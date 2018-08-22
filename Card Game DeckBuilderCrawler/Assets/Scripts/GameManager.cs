using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public ObjectPool Pool { get; set; }

    //This is called before Start()
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        TurnManager.Instance.StartCoroutine(TurnManager.Instance.DrawInitialHand());
    }

    public void MakeCardTEST()
    {
       // Card newCard = new Card("Attack", "+1 Attack", 1);
        // newCard.cardPrefab.transform.parent = this.transform;
    }
}
