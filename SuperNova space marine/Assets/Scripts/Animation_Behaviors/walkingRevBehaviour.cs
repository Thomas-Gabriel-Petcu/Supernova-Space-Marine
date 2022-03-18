using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingRevBehaviour : StateMachineBehaviour
{
    GameObject player;
    Player_Movement movement;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Player_Movement>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
            if (movement.enabled == false)
            {
                animator.SetBool("climbingLadder", true);
            }
            switch (movement.moveInput)
        {
            case 0://stationary - proceed to idle
                animator.SetBool("idling", true);
                animator.SetBool("isWalkingRev", false);
                break;
            default:
                break;
        }

        switch (movement.moveInput + player.transform.eulerAngles.y)
        {
            case 1://facing right - moving left
                animator.SetBool("isWalking", true);
                animator.SetBool("isWalkingRev", false);
                break;
            case 179://facing left - moving right
                animator.SetBool("isWalking", true);
                animator.SetBool("isWalkingRev", false);
                break;
            default:
                //math is bad
                break;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
       
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
