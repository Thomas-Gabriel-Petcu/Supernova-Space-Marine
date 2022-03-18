using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPlatformDropdown : MonoBehaviour
{
    public PlatformEffector2D effector;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void OnCollisionStay2D(Collision2D other)
    {


        if (Input.GetKeyDown(KeyCode.S))
        {
            if (other.collider.CompareTag("Player"))
            {
                effector.rotationalOffset = 180f;
            }
        }

    }
    void OnCollisionExit2D(Collision2D other)
    {
        effector.rotationalOffset = 0f;
    }
}

