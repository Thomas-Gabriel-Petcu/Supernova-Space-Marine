using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inst_OnPress_Q : MonoBehaviour
{
    public GameObject objectToInst;
    float timeBtwShots;
    public float startTimeBtwShots = 2;
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Instantiate(objectToInst, transform.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }
}
