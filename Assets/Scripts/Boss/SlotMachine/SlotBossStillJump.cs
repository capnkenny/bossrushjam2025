using UnityEngine;

public class SlotBossStillJump : StateMachineBehaviour
{
    [SerializeField] private string launchingTriggerName;
    private Animator reelAnimator;
    [SerializeField] private string tagNameForReel;
    [SerializeField] private bool stopAttack;
    [SerializeField] private float yValueForMax;
    [SerializeField] private float vertSpeed = 7;
    private bool up = true;

    private SlotBossMechanics mech;
    
    private Vector3 pos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrWhiteSpace(launchingTriggerName))
            animator.ResetTrigger(launchingTriggerName);
        reelAnimator = GameObject.FindGameObjectWithTag(tagNameForReel).GetComponent<Animator>();    
        pos = animator.transform.position;
        mech = animator.gameObject.GetComponent<SlotBossMechanics>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float newY = vertSpeed*Time.deltaTime;
		
        if(animator.transform.position.y + newY > yValueForMax)
        {
           up = false;
        }
        var nextPos = new Vector3();
        if(up)
        {
            nextPos = new Vector3(animator.transform.position.x, animator.transform.position.y + newY, animator.transform.position.z);
        }
        else
        {
            if(animator.transform.position.y - newY >= pos.y)
            {
                nextPos = new Vector3(animator.transform.position.x, animator.transform.position.y - newY, animator.transform.position.z);
            }
            else
            {
                nextPos = pos;
                if(mech)
                {
                    mech.PlayJumpSound();
                }
            }
        }
		
        animator.transform.position = nextPos;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        up = true;
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
    }

}
