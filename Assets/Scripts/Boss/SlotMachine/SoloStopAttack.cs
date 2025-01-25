using UnityEngine;

public class SoloStopAttack : StateMachineBehaviour
{
    [SerializeField] private bool stopAttack;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(stopAttack)
        animator.SetBool("attacking", false);
    }
}

