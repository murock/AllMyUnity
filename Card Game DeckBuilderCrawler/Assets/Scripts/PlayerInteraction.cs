using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : Singleton<PlayerInteraction> {

    // Player Name
    [SerializeField]
    private Text playerNameTxt;
    // player Health
    [SerializeField]
    private Text playerHealthTxt;

    //numeric value of player health
    [SerializeField]
    private int playerHealth;

    public int PlayerHealth
    {
        get
        {
            return this.playerHealth;
        }
    }
        
    private void Start()
    {
        this.UpdateHP();
    }

    public void AddDefence(int defence)
    {
        this.playerHealth += defence;
        this.UpdateHP();

        //This kind of logic probably only applies in something that both takes away and adds to health
        //if (this.playerHealth > 0)
        //{
        //     this.playerHealthTxt.text = string.Format("HP: <color=green>{0}</color>", this.playerHealth.ToString());
        //}    
        //else
        //{
        //    this.playerHealth = 0;
        //    this.playerHealthTxt.text = "You Died";
        //    this.playerHealthTxt.text = string.Format("HP: <color=green>{0}</color>", this.playerHealth.ToString());
        //}
    }

    public void TakeDamage(int damage)
    {
        this.playerHealth -= damage;
        this.UpdateHP();
    }

    private void UpdateHP()
    {
        if (this.playerHealth > 0)
        {
            this.playerHealthTxt.text = string.Format(this.playerHealth.ToString());
        }
        else
        {
            this.playerHealthTxt.text = string.Format("<color=red>Player Dead</color>");
        }

    }

}
