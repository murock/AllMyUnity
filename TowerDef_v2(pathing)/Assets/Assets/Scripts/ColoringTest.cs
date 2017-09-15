using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColoringTest : MonoBehaviour {

    [SerializeField]
    private Color testcolor;
	// Use this for initialization
	void Start () {


	}

    void Update()
    {
        Image test = GetComponent<Image>();
        test.color = testcolor;
    }
	
}
