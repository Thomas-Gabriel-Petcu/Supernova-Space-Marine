using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Missile_Homing : MonoBehaviour
{
    public Transform target;
    Vector3 direction;
    float angle;
    Rigidbody2D rb;
    public bool isLeft;
    public bool isRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enabled = false;
    }
    
    void Start()
    {
        SFX_Manager.instance.Play("Grunt_Missile_Launch");
        if (isLeft == true)
        {
            Left();
        }
        if (isRight == true)
        {
            Right();
        }
         
        Invoke("SwitchTarget", 1.5f);
    }

    void Update()
    {      
        direction = target.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 3);

    }
    void FixedUpdate()
    {
        rb.velocity = transform.right * 1000 * Time.deltaTime;
    }
    void SwitchTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Left()
    {
        target = GameObject.FindGameObjectWithTag("LeftTarget").transform;
    }
    void Right()
    {
        target = GameObject.FindGameObjectWithTag("RightTarget").transform;
    }
}
