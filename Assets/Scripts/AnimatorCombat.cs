using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCombat : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("moving", false);
    }

   
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EnemyAI ec = animator.GetComponentInParent<EnemyAI>();
        if (ec.dead) return;
        Collider2D[] enemiesToDmg = Physics2D.OverlapBoxAll(ec.getAttackPosition(), new Vector2(ec.atkRangeX, ec.atkRangeY), 0, ec.Damageble);
        foreach (Collider2D enemy in enemiesToDmg)
        {
            if (enemy.GetComponent<PlayerCombat>())
            {
                enemy.GetComponent<PlayerCombat>().TakeDamage(ec.dmg);
            }
        }
        animator.SetBool("moving", true);
        animator.SetInteger("attackDirection", -1);
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
