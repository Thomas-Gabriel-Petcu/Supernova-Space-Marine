using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Ladder_Tutorial : MonoBehaviour
{
    public GameObject tut;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tut.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
