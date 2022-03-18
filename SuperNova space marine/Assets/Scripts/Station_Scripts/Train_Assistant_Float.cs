using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train_Assistant_Float : MonoBehaviour
{
    Vector3 pos;
    Vector3 posTop;
    Vector3 posBottom;
    bool canMoveUp = false;
    float speed = 1f;
    float timer;
    
    void Awake()
    {
        timer = speed;
        posTop = transform.position + new Vector3(0, 0.1f, 0);
        posBottom = transform.position + new Vector3(0, -0.1f, 0);
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (canMoveUp)
        {
            transform.position = Vector3.Slerp(transform.position, posTop, speed * Time.deltaTime);
            if (timer <= 0)
            {
                canMoveUp = !canMoveUp;
                timer = speed;
            }

        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, posBottom, speed * Time.deltaTime);
            if (timer <= 0)
            {
                canMoveUp = !canMoveUp;
                timer = speed;
            }
        }
    }
}
