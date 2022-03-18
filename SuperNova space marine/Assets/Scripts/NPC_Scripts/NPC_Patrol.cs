using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    float timer;
    public float startTimer;
    public float speed;
    public bool movingRight;
    void Start()
    {
        timer = startTimer;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            movingRight = !movingRight;
            timer = startTimer;
        }
        if (movingRight)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(Vector2.right * speed);
        }
        else
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(Vector2.right * speed);
        }

    }

}
