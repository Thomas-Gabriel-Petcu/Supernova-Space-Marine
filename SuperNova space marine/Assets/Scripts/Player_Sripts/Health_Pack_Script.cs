using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Pack_Script : MonoBehaviour
{
    float healAmount;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            healAmount = Random.Range(2, 10);
            other.GetComponent<Player_Health>().health += healAmount;
            Destroy(gameObject);
        }
    }
}
