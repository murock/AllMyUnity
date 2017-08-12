using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private List<GameObject> allPickUps;
    private string buttonMovement;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        allPickUps = new List<GameObject>();
        buttonMovement = "";
    }

    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("in key press down");
            foreach ( GameObject pickUp in allPickUps)
            {
                Debug.Log("looking at object " + pickUp.name);
                pickUp.SetActive (true);
                count--;
            }
        }
    }
    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (buttonMovement == "down")
        {
            Debug.Log("Down");
            moveVertical = -1;
        } else if ( buttonMovement == "up")
        {
            Debug.Log("UP");
            moveVertical = 1;
        } else
        {
            moveVertical = Input.GetAxis("Vertical");
        }

        if (buttonMovement == "right")
        {
            moveHorizontal = 1;
        } else if ( buttonMovement == "left")
        {
            moveHorizontal = -1;
        }else
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);



    }

    public void updateMovement (string movement)
    {
        if(movement == "down")
        {
            buttonMovement = "down";
        } else if(movement == "up")
        {
            buttonMovement = "up";
        } else if(movement == "right")
        {
            buttonMovement = "right";
        } else if(movement == "left")
        {
            buttonMovement = "left";
        }
    }

    public void releasebutton ()
    {
        buttonMovement = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            Debug.Log("Pick up is : " + other.gameObject.name);
            allPickUps.Add(other.gameObject);
            other.gameObject.SetActive (false);
            count++;
            SetCountText();
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
