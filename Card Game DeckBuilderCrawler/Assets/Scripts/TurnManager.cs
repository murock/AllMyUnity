using System.Collections;
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

    [SerializeField]
    private Text spawnText;
    [SerializeField]
    private int defaultSpawnCount = 2;
    private int spawnCount;
    [SerializeField]
    private Text monstersLeftText;
    [SerializeField]
    private int defaultMonstersLeft = 5;
    private int monstersLeft;
    [SerializeField]
    public List<MonsterInteraction> monsterSpawners;
    [SerializeField]
    private Button turnButton;

    private Queue<Monster> monstersQueue = new Queue<Monster>();

    public int MonstersLeft
    {
        get
        {
            return this.monstersLeft;
        }
    }
    public int SpawnCount
    {
        get
        {
            return this.spawnCount;
        }
        set
        {
            if (value == -1)
            {
                //Reset spawn count
                this.spawnCount = this.defaultSpawnCount;
                spawnText.text = "Next Spawn: " + this.spawnCount.ToString();
                //spawn new monster
                foreach (MonsterInteraction monster in monsterSpawners)
                {
                    if (monster != null && !monster.IsAlive && this.monstersLeft > 0)
                    {
                        //if monster is not alive ie not yet spawned then spawn
                        monster.Spawn(monstersQueue.Dequeue());
                        this.monstersLeft--;
                        this.monstersLeftText.text = "Monsters Left: " + this.monstersLeft.ToString();
                        return;
                    }
                }

            }
            else
            {
                this.spawnCount = value;
                spawnText.text = "Next Spawn: " + this.spawnCount.ToString();
            }

        }
    }



    public TurnManager()
    {

    }

    private void Start()
    {
        this.spawnCount = this.defaultSpawnCount;
        this.spawnText.text = "Next Spawn: " + this.spawnCount.ToString();
        this.monstersLeft = this.defaultMonstersLeft;
        this.monstersLeftText.text = "Monsters Left: " + this.monstersLeft.ToString();
        this.createIntialMonsters();
        if (monsterSpawners.Count > 0)
        {
            //Spawn the first monster
            monsterSpawners[0].Spawn(monstersQueue.Dequeue());
            GameManager.Instance.currentMonster = monsterSpawners[0];
        }
    }

    private void createIntialMonsters()
    {
        for (int i = 0; i < this.defaultMonstersLeft; i++)
        {
            Monster monster;
            if (i > 2)
            {
                monster = new Monster(4, 2, "Troll", "Enemies/Troll1", null);
            }
            else
            {
                BlockDrawMech blockDrawMech = new BlockDrawMech();
                monster = new Monster(2, 4, "Ice Golem", "Enemies/IceGolem", blockDrawMech);
            }
            monstersQueue.Enqueue(monster);
        }
    }

    //Called when the end turn button is hit
    public void EndTurn()
    {
        this.turnButton.enabled = false;
        GameManager.Instance.multiplierNum = 1;
        GameManager.Instance.multiplierOn = false;
        Hand.Instance.DiscardHand();
        foreach (MonsterInteraction monster in this.monsterSpawners)
        {
            if (monster.IsAlive)
            {
                monster.DoDamage();
            }
        }
        DiscardCardsInPlay();
        StartCoroutine(CardDraw.Instance.DrawCards());
        SpawnCount -= 1;
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
