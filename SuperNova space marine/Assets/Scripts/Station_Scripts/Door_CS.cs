using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_CS : MonoBehaviour
{
    public float moveSpeed;
    public float openPosY;
    public float closedPosY;
    bool Opening = false;
    void Update()
    {
        if (Opening == true && transform.position.y <= openPosY)
        {
            transform.Translate(Vector2.up * moveSpeed);
        }
        else if (Opening == false && transform.position.y >= closedPosY)
        {
            transform.Translate(Vector2.down * moveSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        Opening = true;
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        Opening = false;
    }
}
