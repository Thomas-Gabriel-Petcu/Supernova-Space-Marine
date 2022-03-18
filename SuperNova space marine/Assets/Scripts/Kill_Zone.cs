using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill_Zone : MonoBehaviour
{
    public GameObject deathMenuUI;
 void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            deathMenuUI.SetActive(true);
        }
    }
}
