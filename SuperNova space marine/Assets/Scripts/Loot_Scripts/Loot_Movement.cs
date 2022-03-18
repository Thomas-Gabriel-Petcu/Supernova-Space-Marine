using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot_Movement : MonoBehaviour
{
    public float speed; //assign in inspector
    public Transform target; //assign as player's position
    
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
