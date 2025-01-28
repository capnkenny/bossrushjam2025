using UnityEngine;

public class ResetReels: StateMachineBehaviour
{
    [SerializeField] private string tagNameForReel1;
    [SerializeField] private string tagNameForReel2;
    [SerializeField] private string tagNameForReel3;
    private Animator reelAnim1;
    private Animator reelAnim2;
    private Animator reelAnim3;
    private const string SpinAction = "Spin";
    
     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        reelAnim1 = GameObject.FindGameObjectWithTag(tagNameForReel1).GetComponent<Animator>();
        reelAnim2 = GameObject.FindGameObjectWithTag(tagNameForReel2).GetComponent<Animator>();
        reelAnim3 = GameObject.FindGameObjectWithTag(tagNameForReel3).GetComponent<Animator>();
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (reelAnim1 != null)
        {
            reelAnim1.SetTrigger(SpinAction);
        }
        if (reelAnim2 != null)
        {
            reelAnim2.SetTrigger(SpinAction);
        } 
        if (reelAnim3 != null)
        {
            reelAnim3.SetTrigger(SpinAction);
        }
        if(animator.transform.position.y != 22.65f)
            animator.transform.position = new Vector3(animator.transform.position.x, 
                22.65f,
                animator.transform.position.z);
    }
}

