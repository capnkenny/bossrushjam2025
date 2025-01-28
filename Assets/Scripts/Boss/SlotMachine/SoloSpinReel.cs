using UnityEngine;

public class SoloSpinReel: StateMachineBehaviour
{
    [SerializeField] private string tagNameForReel;
    private Animator reelAnimator;
    
     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        reelAnimator = GameObject.FindGameObjectWithTag(tagNameForReel).GetComponent<Animator>();    
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
          
    }
}

