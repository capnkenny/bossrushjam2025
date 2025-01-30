using System.Collections.Generic;
using UnityEngine;

public class SlotBossIdleAttack : StateMachineBehaviour
{
    [SerializeField] private float attackWaitDurationSeconds = 2.5f;
    [SerializeField] private List<string> triggersToLaunch;

    private float timeToWait;
    
    
    private float passedTime = 0.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {  
      timeToWait = attackWaitDurationSeconds;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      if(!animator.GetBool("attacking"))
      {
       passedTime += Time.deltaTime;

       if(passedTime >= timeToWait)
       {
          int trigger = Random.Range(0, triggersToLaunch.Count);
          animator.SetTrigger(triggersToLaunch[trigger]);
          animator.SetBool("attacking", true);
          passedTime = 0;
          if((trigger + 1) % 2 == 0)
          {
            timeToWait = attackWaitDurationSeconds + 1.0f;
          }
          else
          {
            timeToWait = attackWaitDurationSeconds;
          }
       }
      }
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
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
