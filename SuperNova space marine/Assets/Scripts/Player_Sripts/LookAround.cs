using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    Vector3 mouse_pos; //stores a position value
    public Transform target; //Assign to the object you want to rotate
    Vector3 object_pos; //stores a position value
    float angle; //single floating point used for storring the angle

    public bool facingRight = true;


    void Update()
    {
        
        mouse_pos = Input.mousePosition; //assigns the mouse position value the variable
        mouse_pos.z = 0f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (angle < -90 && angle > -180 || angle > 90 && angle < 180)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            facingRight = false;
        }
        //if functions check where the gun is pointing and turns the entire player depending on it
        if (angle < 91 && angle > -91)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            facingRight = true;
        }
        if (angle > 89 && angle < 90)
        {
            return;
        }
    }
}

