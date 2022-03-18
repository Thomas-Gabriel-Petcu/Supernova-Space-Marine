using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer_Turret : MonoBehaviour
{
    
    public Transform player;
    private Rigidbody2D rb;
    public float rotateSpeed;
    float rotateAmount;

    public bool canRotate;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

    }


    void FixedUpdate()
    {
        if (canRotate == true)
        {
            Vector2 direcion = (Vector2)player.position - rb.position;
            direcion.Normalize();
            rotateAmount = Vector3.Cross(direcion, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;
        }
       
    }


}
