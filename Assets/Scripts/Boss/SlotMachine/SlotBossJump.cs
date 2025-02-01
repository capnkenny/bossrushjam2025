using UnityEngine;

public class SlotBossJump : StateMachineBehaviour
{
    [SerializeField] private string launchingTriggerName;
    [SerializeField] private Vector3 positionToJumpTo;
    [SerializeField] private float durationOfJumpInSeconds = 1;
    [SerializeField] private float arcHeight = 1;
    [SerializeField] private float horizontalSpeed = 10;
    private Animator reelAnimator;
    [SerializeField] private string tagNameForReel;
    [SerializeField] private bool stopAttack;
    
    private Vector3 originalPosition;
    private GameObject boss;
    private SlotBossMechanics mech;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.FindGameObjectWithTag("SlotBoss");
        originalPosition = boss.transform.position;
        reelAnimator = GameObject.FindGameObjectWithTag(tagNameForReel).GetComponent<Animator>();
        mech = animator.gameObject.GetComponent<SlotBossMechanics>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float x0 = originalPosition.x;
	    float x1 = positionToJumpTo.x;
		float dist = x1 - x0;
        
		float nextX = Mathf.MoveTowards(boss.transform.position.x, x1, horizontalSpeed * Time.deltaTime);
		float baseY = Mathf.Lerp(originalPosition.y, positionToJumpTo.y, (nextX - x0) / dist);
		float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
		var nextPos = new Vector3(nextX, baseY + arc, boss.transform.position.z);

		boss.transform.SetPositionAndRotation(nextPos, boss.transform.rotation);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrWhiteSpace(launchingTriggerName))
            animator.ResetTrigger(launchingTriggerName);
        if (reelAnimator != null)
        {
            string triggerName = string.Empty;
            switch(Random.Range(1, 6))
            {
                case 1:
                {
                    triggerName = "Diamond";
                    break;
                }
                case 2:
                {
                    triggerName = "Coconut";
                    break;
                }
                case 3:
                {
                    triggerName = "Cherry";
                    break;
                }
                case 4:
                {
                    triggerName = "Bell";
                    break;
                }
                case 5:
                {
                    triggerName = "Bar";
                    break;
                }
                default:
                    break;
            }
            if (reelAnimator != null && !string.IsNullOrWhiteSpace(triggerName))
            {
                reelAnimator.SetTrigger(triggerName);
            }
        }
            
        if(stopAttack)
            animator.SetBool("attacking", false);

        if(mech)
        {
            mech.PlayJumpSound();
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
