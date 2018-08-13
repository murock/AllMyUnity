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
    private int health;

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

    private void Start()
    {
        this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if (this.health <= 0)
        {
            this.health = 0;
            this.nameTxt.text = "DEFEATED!!!";
            this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());
        }
        else
        {
            this.healthTxt.text = string.Format("HP: <color=red>{0}</color>", this.health.ToString());
        }
    }
}
