using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour {


    //    I see three main options:

    //1) Triggers

    //Add a collider to the "other" object. Make it a trigger.Attach a script to it. In that script you'll get OnTrigger* events (e.g., you'll probably be interested in OnTriggerExit).

    //You can move your character however you need to when you get the OnTriggerExit event.

    //2) Physics

    //Add a rigidbody and a collider to your character. Now it'll collide with any (non-trigger) colliders in the scene. Colliders can't be inside of each other, so you'll need to surround your object with a number of colliders (e.g., to cover all sides of a box you'd need six).

    //This way requires more setup, but you get the benefit of physics handling the movement for you (at least partly).

    //3) Hybrid

    //One interesting way to approach this would be to use triggers (option 1) to detect when you character is out-of-bounds, then dynamically spawn a physics collider positioned so that they'll bounce off it (similar to option 2, but dynamic).

    //That would avoid the setup overhead of creating six colliders, and would handle dynamic situations better. But getting the spawning working correctly will be painful I'd guess =]
    [SerializeField]
    private float playerSpeed = 2;

    private Animator myAnimator;
    // Use this for initialization
    void Start () {
        myAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        playerMovement();
	}

    private void playerMovement()
    {
        float horiDirection = Input.GetAxisRaw("Horizontal");
        float vertDirection = Input.GetAxisRaw("Vertical");
        myAnimator.SetBool("isMoving", true);
        if (0 < horiDirection && vertDirection == 0)     //Right
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        else if (0 > horiDirection && vertDirection == 0)    //Left
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        else if (0 < vertDirection && horiDirection == 0)    //up
        {
            transform.Translate(Vector3.up * playerSpeed * Time.deltaTime);
        }
        else if (0 > vertDirection && horiDirection == 0)     //down
        {
            transform.Translate(Vector3.down * playerSpeed * Time.deltaTime);
        }
        else if (0 < horiDirection &&  0 < vertDirection)   //up and right
        {
            transform.Translate(new Vector3(0.5f, 0.5f, 0f) * playerSpeed * Time.deltaTime);
        }
        else if (0 < horiDirection && 0 > vertDirection) //down and right
        {
            transform.Translate(new Vector3(0.5f, -0.5f, 0f) * playerSpeed * Time.deltaTime);
        }
        else if (0 > horiDirection && 0 < vertDirection)  //up and left
        {
            transform.Translate(new Vector3(-0.5f, 0.5f, 0f) * playerSpeed * Time.deltaTime);
        }
        else if (0 > horiDirection && 0 > vertDirection)    //down and left
        {
            transform.Translate(new Vector3(-0.5f, -0.5f, 0f) * playerSpeed * Time.deltaTime);
        }
        else
        {
            //idle
            myAnimator.SetBool("isMoving", false);
        }

    }
}
