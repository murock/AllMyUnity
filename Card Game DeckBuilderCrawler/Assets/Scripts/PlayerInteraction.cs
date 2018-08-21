﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour {

    // Player Name
    [SerializeField]
    private Text playerNameTxt;
    // player Health
    [SerializeField]
    private Text playerHealthTxt;

    [SerializeField]
    private int playerHealth;

    public Text PlayerNameTxt
    {
        get
        {
            return this.playerNameTxt;
        }
        set
        {
            this.playerNameTxt = value;
        }
    }
    public Text PlayerHealthtxt
    {
        get
        {
            return this.playerHealthTxt;          
        }
        set
        {
            this.playerHealthTxt = value;
        }
    }

    private void Start()
    {
        this.playerHealthTxt.text = string.Format("HP: <color=red>{0}</color>", this.playerHealth.ToString());
    }

    public void AddDefence(int defence)
    {
        this.playerHealth += defence;
        this.playerHealthTxt.text = string.Format("HP: <color=green>{0}</color>", this.playerHealth.ToString());

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




}
