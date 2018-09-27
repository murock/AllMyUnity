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


    private Vector3 startPos;

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
            this.health = 0;
            this.nameTxt.text = "DEFEATED!!!";
            this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());
            GameManager.Instance.centrePanel.SetActive(true);
            ShopManager.Instance.StartShop();
        }
        else
        {
            this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());
        }
    }

    public void DoDamage()
    {
        StartCoroutine(MoveMonster());
    }

    private IEnumerator MoveMonster()
    {

        var endPos = GameManager.Instance.player.transform.position;
        var speed = 1000f;

        while (this.transform.position != endPos)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, endPos, speed * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
        }
        this.transform.position = this.startPos;
        PlayerInteraction.Instance.TakeDamage(this.attack);
        GameManager.Instance.centrePanel.SetActive(true);
        ShopManager.Instance.StartShop();
        // this.transform.position = startPos;
    }
}
