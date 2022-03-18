using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject player;
    Rigidbody2D playerRB;
    private Vector3 vec;
    public int slownessMultiplier;
    void Start()
    {
     playerRB = player.GetComponent<Rigidbody2D>();     
    }

    
    void FixedUpdate()
    {
        vec = playerRB.velocity;
        gameObject.transform.Translate(- vec.x/slownessMultiplier, 0, 0);
    }
}
