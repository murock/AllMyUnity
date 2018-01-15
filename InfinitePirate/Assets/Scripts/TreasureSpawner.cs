using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner: MonoBehaviour {

    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private float spawnRate = 5;

    private float timeSinceTick;

    [SerializeField]
    private GameObject treasurePrefab;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        timeSinceTick += Time.deltaTime;

        if (timeSinceTick >= spawnRate)
        {
            timeSinceTick = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        int spawnIndex = Random.Range(0, 4);
        Instantiate(treasurePrefab, spawnPoints[spawnIndex].transform.position, Quaternion.identity);
    }
}
