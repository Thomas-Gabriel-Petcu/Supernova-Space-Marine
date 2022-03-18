using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Movement : MonoBehaviour
{   public float speed;
    public float stopDist;
    public float retrDist;
    public Vector3 direction;
    public float angle;
    public bool canMove = true;
    public bool canRotate = false;

    GameObject player;
    public Vector3 playerPos;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enabled = false; //disables this script upon instantiating the enemy
    }
    void OnBecameVisible()
    {
        enabled = true; //enables script upon becoming visible to player
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerPos) > stopDist && canMove == true) //checks if distance to player is larger than stopping distance
        {

            transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime); //moves towards the player
        }
        else if (Vector2.Distance(transform.position, playerPos) < stopDist && Vector2.Distance(transform.position, playerPos) > retrDist && canMove == true) //checks if the enemy is within both boundaries
        {
            canRotate = true;

            transform.position = this.transform.position; //stands still          
        }
        else if (Vector2.Distance(transform.position, playerPos) < retrDist && canMove == true) //checks if distance to player is smaller than retreat distance
        {

            transform.position = Vector2.MoveTowards(transform.position, playerPos, -speed * Time.deltaTime); //moves away from player
        }
    }

    void Update()
    {    
            playerPos = player.transform.position;

        direction = playerPos - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);   //rotates to face the player 
    }   
}
