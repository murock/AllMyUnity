using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {



    //To be implmented still for efficency
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
