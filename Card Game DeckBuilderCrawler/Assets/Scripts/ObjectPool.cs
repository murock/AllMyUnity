using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    //used to hold objects so they don't need to be created and destroyed all the time
    [SerializeField]
    private List<GameObject> objectPrefabs;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public GameObject GetObject(string type)
    {

        foreach (GameObject go in pooledObjects)
        {
            if (go.name == type && !go.activeInHierarchy)    //if name matches type we want to spawn and is inactive then re-use
            {
                go.SetActive(true);
                return go;
            }
        }

        //creates a new object if one isn't in the object pool
        for (int i = 0; i < objectPrefabs.Count; i++)
        {
            if (objectPrefabs[i].name == type)
            {
                GameObject newObject = Instantiate(objectPrefabs[i]);
                pooledObjects.Add(newObject);
                newObject.name = type;
                return newObject;
            }
        }
        return null;
    }

    //Put an object in the objectPool
    public void ReleaseObject(GameObject gameObject)
    {
        gameObject.SetActive(false);

    }

    public void AddObject(GameObject gameObject)
    {
        bool typeExists = false;
        for (int i = 0; i < objectPrefabs.Count; i++)
        {
            if (objectPrefabs[i].name == gameObject.name)
            {
                typeExists = true;
            }
        }
        //If this type of gameobject is not in pool add it to the pool
        if (!typeExists)
        {
            objectPrefabs.Add(gameObject);
        }
    }
}
