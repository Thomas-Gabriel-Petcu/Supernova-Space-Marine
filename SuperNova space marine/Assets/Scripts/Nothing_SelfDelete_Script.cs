using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nothing_SelfDelete_Script : MonoBehaviour
{
    public float time;
    void Start()
    {
        Invoke("Deactivate", time);
       //Destroy(gameObject, time);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
