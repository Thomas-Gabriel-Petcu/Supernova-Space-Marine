using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftOpenBehavior : StateMachineBehaviour
{
    Destroyer_Missile_Handler handler;
    float timer = 2.2f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("close", false);
        handler = GameObject.Find("Rocket_launcher_Left").GetComponent<Destroyer_Missile_Handler>();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        handler.FireMissiles();
        if (timer <= 0 && handler.isReplenished == false)
        {
            animator.SetBool("close", true);
        }
        
    }

  
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

  
}
