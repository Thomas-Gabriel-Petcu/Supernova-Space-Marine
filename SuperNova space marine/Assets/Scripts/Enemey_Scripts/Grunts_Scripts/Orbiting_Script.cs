using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting_Script : MonoBehaviour
{
    //float timer = 0;

    //Grunt_Movement movement;

    //void Start()
    //{      
    //    movement = GetComponent<Grunt_Movement>();
    //}

    //void Update()
    //{

    //    if (movement.canRotate == true)
    //    { //circle the player
    //        movement.canMove = false;
    //        timer += Time.deltaTime;
    //        float x = Mathf.Cos(timer) * movement.retrDist;
    //        float y = Mathf.Sin(timer) * movement.retrDist;
    //        float z = 0;

    //        transform.position = new Vector3(x, y, z);

    //        if (Vector2.Distance(transform.position, movement.playerPos.position) > movement.retrDist)
    //        {//player is too close and enemy must retreat

    //            movement.canRotate = false;
    //            movement.canMove = true;

    //        }

    //    }

    //}

    //---------------------------------------------------------------
    public float speed;
    public GameObject player;
    Vector3 playerPosition;
    Grunt_Movement movement;
    void Start()
    {
        enabled = false;
        movement = GetComponent<Grunt_Movement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnBecameVisible()
    {
        enabled = true; //enables script upon becoming visible to player
    }

    void Update()
    {
        
        if (movement.canRotate == true)
        {
            movement.canMove = false;
            playerPosition = player.transform.position;

            Rotate();
        }
        if (Vector2.Distance(transform.position, movement.playerPos) > movement.retrDist || Vector2.Distance(transform.position, movement.playerPos) < movement.stopDist)
        {//player is too close and enemy must retreat

            movement.canRotate = false;
            movement.canMove = true;

        }


    }

    void Rotate()
    {
        transform.RotateAround(playerPosition, Vector3.forward, speed * Time.deltaTime);
    }

}
