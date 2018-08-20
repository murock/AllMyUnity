using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    public void MakeCardTEST()
    {
       // Card newCard = new Card("Attack", "+1 Attack", 1);
        // newCard.cardPrefab.transform.parent = this.transform;
    }
}
