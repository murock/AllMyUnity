using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { EARTH, FIRE, WATER, POISON, NONE}

public abstract class Tower : MonoBehaviour {   //abstract means it cannot be standalone other classes must inherit from it

    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float debuffDuration;

    [SerializeField]
    private float proc;     //chance to proc debuff

    public Element ElementType { get; protected set; }

    public int Price { get; set; }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }


    private SpriteRenderer mySpriteRenderer;

    private Monster target;

    public Monster Target
    {
        get { return target; }
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public float DebuffDuration
    {
        get
        {
            return debuffDuration;
        }

        set
        {
            debuffDuration = value;
        }
    }

    public float Proc
    {
        get
        {
            return proc;
        }

        set
        {
            proc = value;
        }
    }

    private Queue<Monster> monsters = new Queue<Monster>();

    private bool canAttack = true;

    private float attackTimer;

    [SerializeField]
    private float attackCooldown;

	// Use this for initialization
	void Start () {
     
        mySpriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Attack();
	}

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    private void Attack()
    {

        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
        //priority system attacks first one to enter range
        if (target == null && monsters.Count > 0)  //if we have no target, but there are more monsters in the Q
        {
            target = monsters.Dequeue();    //make target next in Q
        }

        if (target != null)
        {
            if (target != null && target.IsActive)  //dont attack dead or despawned monster
            {
                if (canAttack)
                {
                    Shoot();
                    canAttack = false;
                }
            }
            else if (monsters.Count > 0)
            {
                target = monsters.Dequeue();
            }
            else if (target != null && !target.Alive || !target.IsActive)
            {
                target = null;  //if target is dead then de target
            }
        }

    }

    private void Shoot()
    {
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {
            monsters.Enqueue(other.GetComponent<Monster>());    //add a monster to the Q
        }     
        
    }

    public abstract Debuff GetDebuff(); //return a debuff od a specfic type MUST be inherited as abstract

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            target = null;
        }
    }
}
