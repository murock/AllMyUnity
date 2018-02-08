using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballController : MonoBehaviour {

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    internal Vector3 direction;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            transform.Translate(direction * speed * Time.deltaTime);
    }
}
