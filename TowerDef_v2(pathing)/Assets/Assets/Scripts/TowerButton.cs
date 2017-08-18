using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int price;

    [SerializeField]
    private Text priceTxt;

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }

    }

    public int Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    private void Start()
    {
        priceTxt.text = Price + "<color=yellow> G </color>";
    }
}
