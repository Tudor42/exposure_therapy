using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set a trigger to transition back to the same state
        animator.SetTrigger("LoopTrigger");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set a trigger to transition back to the same state
        animator.SetTrigger("LoopTrigger");
    }

    // Other methods are left commented out for brevity
}
