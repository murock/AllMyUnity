using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInteraction : MonoBehaviour {

    //Name of monster
    [SerializeField]
    private Text nameTxt;

    //Description of monster
    [SerializeField]
    private Text descriptionTxt;

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

    private bool isAlive = false;

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


    public void Spawn(int health = 2, int attack = 4, string name = "Monster")
    {
        this.health = health;
        this.attack = attack;
        this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", health.ToString());
        this.attackTxt.text = string.Format("Attack: {0}", attack.ToString());
        this.nameTxt.text = name;
        this.startPos = transform.position;
        this.isAlive = true;
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Defeat()
    {
        this.isAlive = false;
        this.health = 0;
        this.nameTxt.text = "DEFEATED!!!";
        this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());

        this.isAlive = false;
        CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        if (IsLastStanding())
        {
            //If monster is the last one standing then spawn another
            TurnManager.Instance.SpawnCount = -1;
        }
        //Instead of opening shop, could open centre panel with some 'Loot' to pick up??
        //GameManager.Instance.centrePanel.SetActive(true);
        //ShopManager.Instance.StartShop();
    }

    private bool IsLastStanding()
    {
        bool lastAlive = true;
        foreach (MonsterInteraction monster in TurnManager.Instance.monsters)
        {
            if (monster != null && monster.IsAlive)
            {
                //Another monster is Alive
                lastAlive = false;
            }           
        }
        return lastAlive;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.Defeat();
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

        //This should be moved to the turn manager doesn't make sense  for it to be in MoveMonster()
        if (GameManager.Instance.Money > 0)
        {
            GameManager.Instance.centrePanel.SetActive(true);
            ShopManager.Instance.StartShop();
        }
    }
}
