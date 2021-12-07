using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorCombat : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*PlayerCombat pc = animator.GetComponent<PlayerCombat>();
        //detect enemies in range
        Collider2D[] enemiesToDmg = Physics2D.OverlapBoxAll(pc.attackPosition, new Vector2(pc.xRange, pc.yRange), 0f, pc.Damageble);
        //damage enemies
        foreach (Collider2D enemy in enemiesToDmg)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(pc.dmg);
        }*/
        animator.speed = 1;
    }

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
