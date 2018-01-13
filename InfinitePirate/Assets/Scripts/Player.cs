using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject playerShip;

    [SerializeField]
    private float playerSpeed = 3;

    [SerializeField]
    private Sprite rightShipSprite, leftShipSprite, shipSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float direction = Input.GetAxisRaw("Horizontal");
        if (0 < direction)
        {
            this.GetComponent<SpriteRenderer>().sprite = rightShipSprite;
            // this.GetComponent<Transform>().position += new Vector3(0.01f,0,0);
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
           // transform.eulerAngles = new Vector3(0f,0f,-45f);
        }
        else if (0 > direction)
        {
            this.GetComponent<SpriteRenderer>().sprite = leftShipSprite;
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
           // this.GetComponent<Transform>().position -= new Vector3(0.01f, 0, 0);
        }
        else 
        {
            this.GetComponent<SpriteRenderer>().sprite = shipSprite;
        }
    }
}
