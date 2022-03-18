using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //Variables
    public string nameOfPool;
    public bool isDynamic;
    public int pooledAmount;
    public GameObject go;
    public static ObjectPooler instance;
    private List<GameObject> Pool;
    void Awake() // I don't think I'll need a singleton
    {
        Pool = new List<GameObject>();
    }
    //Functions

    void Start()
    {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(go);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            Pool.Add(obj);
        }
    }

    public GameObject GetPooledObject() //returns an object from the pool if it's not already active
    {
        for (int i = 0; i < Pool.Count; i++)
        {
            if (!Pool[i].activeInHierarchy)
            {
                return Pool[i];
            }

        }
        if (isDynamic) //if the pool ran out of objects to provide then another object will be created and pooled
        {
            GameObject obj = Instantiate(go);
            Pool.Add(obj);
            return obj;
        }

        return null;
    }

}
