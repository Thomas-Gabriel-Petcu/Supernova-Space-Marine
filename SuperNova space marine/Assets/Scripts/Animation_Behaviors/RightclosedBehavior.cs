using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightclosedBehavior : StateMachineBehaviour
{
    Destroyer_Missile_Handler handler;
    All_Enemies_Health bossHpCS;
    float timer = 20f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("canOpen", false);
        timer = 20f;
        handler = GameObject.Find("Rocket launcher_Right").GetComponent<Destroyer_Missile_Handler>();
        bossHpCS = GameObject.FindGameObjectWithTag("Boss").GetComponent<All_Enemies_Health>();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if (handler.isReplenished == false && bossHpCS.enemyHp < 1500 && timer <= 0)
        {
            handler.Replenish();
            timer = 20f;
        }
         
        if (handler.isReplenished == true)
        {
            animator.SetBool("canOpen", true);
            
        }
    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

  
}
