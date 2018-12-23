using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster {

    private int health;
    private int attack;
    private string title;
    private string artLocation;
    private IMonsterMech monsterMech;

    public Monster(int health, int attack, string title, string artLocation, IMonsterMech monsterMech)
    {
        this.health = health;
        this.attack = attack;
        this.title = title;
        this.artLocation = artLocation;
        this.monsterMech = monsterMech;
    }

    public IMonsterMech MonsterMech
    {
        get { return monsterMech; }
        set { monsterMech = value; }
    }

    public string ArtLocation
    {
        get { return artLocation; }
        set { artLocation = value; }
    }

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }


}
