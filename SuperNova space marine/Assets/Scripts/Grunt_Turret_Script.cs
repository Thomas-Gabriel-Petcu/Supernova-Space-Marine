using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT NEEDS CHANGING TO MATCH THE ONE ON THE DESTROYER TURRET - results in smooth turret turning.

public class Grunt_Turret_Script : MonoBehaviour
{
    SpriteRenderer sr;
    public Transform playerPos;
    public Vector3 direction;
    public float angle;
    
    void Start()
    {
        sr =gameObject.GetComponent<SpriteRenderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //grabs reference to player's transform
    }

    void Update()
    {
        sr.material = GetComponentInParent<SpriteRenderer>().material;
        direction = playerPos.position - transform.position; //calculates direction from turret to player
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calculates angle from turret to player
       transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward); //rotates the turret towards the player
    }
}
