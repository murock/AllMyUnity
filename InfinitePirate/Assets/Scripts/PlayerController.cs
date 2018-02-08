using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float playerSpeed = 3;

    [SerializeField]
    GameObject cannonBall;

    private WWW test;

    private bool rotatedRight, rotatedLeft;

    // Use this for initialization
    void Start()
    {
        rotatedRight = false;
        rotatedLeft = false;

    }

    // Update is called once per frame
    void Update()
    {
        this.rotationController();
        if (Input.GetButtonDown("Fire1"))
        {
            this.firePortCannon();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            this.fireStarboardCannon();
        }
    }

    private void rotateRight()
    {
        Vector3 rightAngle = new Vector3(0f, 0f, -45f);
        if (!rotatedRight)
        {
            transform.Rotate(rightAngle, Space.World);
            rotatedRight = true;
        }
    }

    private void rotateLeft()
    {
        Vector3 leftAngle = new Vector3(0f, 0f, 45f);
        if (!rotatedLeft)
        {
            transform.Rotate(leftAngle, Space.Self);
            rotatedLeft = true;
        }
    }

    private void rotationController()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        if (0 < direction)
        {
            if (rotatedLeft)   //Ensures left rotation is reset before rotating right
            {
                rotateRight();
            }
            else
            {
                this.rotateRight();
                Vector3 diagonalRight = new Vector3(0.5f, 0.5f, 0);
                transform.Translate(diagonalRight * playerSpeed * Time.deltaTime);
            }
        }
        else if (0 > direction)
        {
            if (rotatedRight)
            {
                rotateLeft();
            }
            else
            {
                this.rotateLeft();
                Vector3 diagonalLeft = new Vector3(-0.5f, 0.5f, 0);
                transform.Translate(diagonalLeft * playerSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (rotatedRight)
            {
                rotateLeft();
            }
            else if (rotatedLeft)
            {
                rotateRight();
            }
            rotatedLeft = false;
            rotatedRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pickUp")
        {
            GameManager.Instance.IncreaseScore();
            collision.gameObject.SetActive(false);
        }
    }

    private void fireStarboardCannon()
    {
        GameObject cannonBallGameObject = Instantiate(cannonBall, transform.position - new Vector3(0,1.1f,0), this.transform.rotation);
        CannonballController cannonScript = cannonBallGameObject.GetComponent<CannonballController>();
        cannonScript.direction =  Vector3.right;
    }

    private void firePortCannon()
    {
        GameObject cannonBallGameObject = Instantiate(cannonBall, transform.position - new Vector3(0, 1.1f, 0), this.transform.rotation);
        CannonballController cannonScript = cannonBallGameObject.GetComponent<CannonballController>();
        cannonScript.direction = Vector3.left;
    }
}
