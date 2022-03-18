using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Killer : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            Destroy(other.collider.gameObject);
        }
    }
}
