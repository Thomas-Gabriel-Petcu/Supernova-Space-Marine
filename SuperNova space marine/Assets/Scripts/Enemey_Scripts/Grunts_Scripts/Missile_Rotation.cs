using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Rotation : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 direction;
    public float angle;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //grabs reference to player's transform
    }


    void Update()
    {
        direction = playerPos.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
    }
}
