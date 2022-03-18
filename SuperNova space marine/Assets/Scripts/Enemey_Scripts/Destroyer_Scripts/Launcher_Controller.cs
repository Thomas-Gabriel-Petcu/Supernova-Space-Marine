using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher_Controller : MonoBehaviour
{
    Animator animator;
    public bool canTestFire;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        if (canTestFire == true)
        {
            animator.SetBool("isOpening", true);
            canTestFire = false;

        }
    }
}
