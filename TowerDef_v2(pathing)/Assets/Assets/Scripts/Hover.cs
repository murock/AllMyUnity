using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover> {

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        this.spriteRenderer = GetComponent<SpriteRenderer>();       
	}
	
	// Update is called once per frame
	void Update () {
        FollowMouse();
	}

    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);   //follow mouse
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);    //sorts out Z position so 2d sprite isn't lost behind other objects
        }
    }

    public void Activate(Sprite sprite)     //allows you to attach the sprite of a tower to the pointer (drag and drop...kinda)
    {
        this.spriteRenderer.sprite = sprite;
        this.spriteRenderer.enabled = true;
    }

    public void Deavtivate()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.ClickedBtn = null;         //turn off so tower isn't placed
    }
}
