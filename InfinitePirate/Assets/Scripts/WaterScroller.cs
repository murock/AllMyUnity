using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroller : MonoBehaviour {

    [SerializeField]
    private float scrollSpeed;

	// Update is called once per frame
	void Update () {
        //float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        //transform.position = startPosition + Vector3.forward * newPosition;

        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);     //returns values 0->1 to set the image position
        Vector2 offset = new Vector2(0, y);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);

    }
}
