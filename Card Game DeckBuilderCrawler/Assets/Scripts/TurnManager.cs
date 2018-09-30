﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : Singleton<TurnManager> {

    //Each turn
    //1) draw 5 cards
    // mulligan cards?
    //2) Play cards -- monster dead check - done in MonsterInteraction
    //3) End Turn - Discard remaining cards
    //4) Monster Attacks
    //5) Check if Monster/Player Dead if so end game
    //6) Repeat 1 to 5
    private int handSize = 5;

    [SerializeField]
    private Text spawnText;
    [SerializeField]
    private int defaultSpawnCount = 2;
    private int spawnCount;

    public int TurnCount
    {
        get
        {
            return this.spawnCount;
        }
        set
        {
            if (value == 0)
            {
                //spawn new monster

                //Reset spawn count
                this.spawnCount = this.defaultSpawnCount;
            }
            this.spawnCount = value;
            spawnText.text = "Next Spawn: " + this.spawnCount.ToString();
        }
    }


    [SerializeField]
    List<MonsterInteraction> monsters;
    public TurnManager()
    {       
    }

    private void Start()
    {
        this.spawnCount = this.defaultSpawnCount;
        spawnText.text = "Next Spawn: " + this.spawnCount.ToString();
    }

    public IEnumerator DrawHand()
    {
        //1) draw 5 cards
        int i = 0;
        //DANGER INFINITE LOOP as i not always incremented CHECK THIS LOGIC (temp)
        while (i < this.handSize)
        {
            //if a card was drawn increment i
            if (CardDraw.Instance.drawCard())
            {
                i++;
            }
            //if there are no cards left to draw increment i
            else if (DeckManager.Instance.cardsInDeck.Count == 0)
            {
                i++;
            }        
            yield return new WaitForSeconds(0.25f);
        }
    }

    //Called when the end turn button is hit
    public void EndTurn()
    {
        GameManager.Instance.multiplierNum = 1;
        GameManager.Instance.multiplierOn = false;
        Hand.Instance.DiscardHand();
        foreach (MonsterInteraction monster in this.monsters)
        {
            if (monster.IsAlive)
            {
                monster.DoDamage();
            }
        }
        DiscardCardsInPlay();
        StartCoroutine(DrawHand());
        TurnCount -= 1;
    }

    private void Mulligan()
    {

    }

    //Discard all cards in play
    private void DiscardCardsInPlay()
    {
        foreach (Transform card in DeckManager.Instance.cardsInPlay)
        {
            //Add card to discarded cards
            DeckManager.Instance.cardsDiscarded.Add(card);
        }
        //Remove cards from cardsinplay
        DeckManager.Instance.cardsInPlay.Clear();
    }
}
