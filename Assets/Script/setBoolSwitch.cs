using System.Collections.Generic;
using UnityEngine;

public class setBoolSwitch : StateMachineBehaviour
{
    private List<string> conditions = new List<string> { "Jumping", "FiringAcorn" };
    private Animator anim;
    private int index;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        index = Random.Range(0, conditions.Count);
        for (int i = 0;i< conditions.Count;i++)
        {
            animator.SetBool(conditions[index], false);
        }
        animator.SetBool(conditions[index], true);
    }


    //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!anim.GetBool(conditions[index]))
        {
            anim.SetBool(conditions[index], true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
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
