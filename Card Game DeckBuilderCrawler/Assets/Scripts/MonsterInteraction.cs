using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInteraction : MonoBehaviour {

    //Name of monster
    [SerializeField]
    private Text nameTxt;

    //Health of monster
    [SerializeField]
    private Text healthTxt;

    [SerializeField]
    private Text attackTxt;

    [SerializeField]
    private int health = 15;

    [SerializeField]
    private int attack = 4;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float travelTime = 2f;

    private Vector3 startPos;
    private float t;

    private bool isAlive = true;

    public Text NameTxt
    {
        get
        {
            return this.nameTxt;
        }
        set
        {
            this.nameTxt = value;
        }
    }

    public Text HealthTxt
    {
        get
        {
            return this.healthTxt;
        }
        set
        {
            this.healthTxt = value;
        }
    }

    public int Attack
    {
        get
        {
            return this.attack;
        }
        set
        {
            this.attack = value;
        }
    }

    public int Health
    {
        get
        {
            return this.health;
        }
    }

    public bool IsAlive
    {
        get
        {
            return this.isAlive;
        }
    }


    private void Start()
    {
        this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());
        this.attackTxt.text = string.Format("Attack: {0}", this.attack.ToString());
        this.startPos = transform.position;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.isAlive = false;
            this.health = 0;
            this.nameTxt.text = "DEFEATED!!!";
            this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());
            //Instead of opening shop, could open centre panel with some 'Loot' to pick up??
            //GameManager.Instance.centrePanel.SetActive(true);
            //ShopManager.Instance.StartShop();
        }
        else
        {
            this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());
        }
    }

    public void DoDamage()
    {
        t = 0;
        StartCoroutine(MoveMonster());
    }

    private IEnumerator MoveMonster()
    {

        var endPos = GameManager.Instance.player.transform.position;

        while (this.transform.position != endPos)
        {
            this.t += Time.deltaTime / this.travelTime;
            this.transform.position = Vector3.Lerp(this.startPos, endPos, this.t);
            yield return new WaitForEndOfFrame();
        }
        this.transform.position = this.startPos;
        PlayerInteraction.Instance.TakeDamage(this.attack);
        GameManager.Instance.centrePanel.SetActive(true);
        ShopManager.Instance.StartShop();
        // this.transform.position = startPos;
    }
}
