using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField]
    private string projectileType;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private int damage;

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
        Debug.Log(target);
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

        if (target != null && target.IsActive)  //dont attack dead or despawned monster
        {
            if (canAttack)
            {
                Shoot();
                canAttack = false;
            }
        }
        else if (monsters.Count >  0)   
        {
            target = monsters.Dequeue();
        }
        if (target != null && !target.Alive)
        {
            target = null;  //if target is dead then de target
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

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            target = null;
        }
    }
}
