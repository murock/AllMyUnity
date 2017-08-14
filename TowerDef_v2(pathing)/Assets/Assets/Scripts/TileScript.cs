using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{

    public Point GridPosition { get; private set; }

    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
        //will only execute when pointer is not over a gameobject 
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }

    }

    private void PlaceTower()
    {
        GameObject tower = Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);     //quanternion so it does not rotate

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y; //stops the towers overlapping 
        tower.transform.SetParent(transform);       //makes the tower a child of the tiles its on

        Hover.Instance.Deavtivate();    //get tower out of hand

        GameManager.Instance.BuyTower();
    }
}
