using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade_Script : MonoBehaviour
{
    Rigidbody2D rb;
    public float force;

    Vector3 mouse_pos; //stores a position value
    Vector3 object_pos; //stores a position value
    float angle; //single floating point used for storring the angle
    public Transform target;

    void Start()
    {

        mouse_pos = Input.mousePosition; //assigns the mouse position value the variable
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * force);
        }
        
    }

