using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorBehaviourShow : StateMachineBehaviour
{
    public Sprite hole;
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
        animator.GetComponent<BossController>().damageble = true;
        GameObject spawningPoint = animator.gameObject.transform.parent.gameObject;
        if(spawningPoint.GetComponent<SpriteRenderer>() == null && this.hole != null)
        {
            //Vector3 holePosition = spawningPoint.transform.position;
            GameObject holeSpriteGO = new GameObject();
            holeSpriteGO.transform.parent = spawningPoint.transform;
            holeSpriteGO.transform.localPosition = new Vector3(0f, .4f, 1f);
            SpriteRenderer holeSprite = holeSpriteGO.AddComponent<SpriteRenderer>();
            holeSprite.sprite = hole;
            holeSprite.sortingOrder = 1;
            holeSprite.sortingLayerName = "Background";
            
        }
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
