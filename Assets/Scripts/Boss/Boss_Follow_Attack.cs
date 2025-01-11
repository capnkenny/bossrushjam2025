using System.Collections;
using UnityEngine;

public class Boss_Follow_Attack : StateMachineBehaviour
{
    [SerializeField] public float speed = 2.5f;
    [SerializeField] public float attackRange = 3f;
    [SerializeField] public float playerPositionYOffset = 1f;
    [SerializeField] public float jumpDistance = 5f;
    [SerializeField] public float jumpSpeed = 3f;
    [SerializeField] public float jumpHeight = 2f;

    
    private Transform player;
    private Rigidbody2D rb;
    private bool isJumping = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isJumping) return;

        Vector2 target = new Vector2(player.position.x, player.position.y + playerPositionYOffset);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if(Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        if(Vector2.Distance(player.position, rb.position) > attackRange + jumpDistance)
        {
            animator.SetTrigger("Jump");
            animator.GetComponent<MonoBehaviour>().StartCoroutine(JumpToPlayer(animator));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Jump");
    }

    private IEnumerator JumpToPlayer(Animator animator)
    {
        isJumping = true;
        Vector2 startPosition = rb.position;
        Vector2 endPosition = new Vector2(player.position.x, player.position.y + playerPositionYOffset);
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * jumpSpeed;
            float height = Mathf.Sin(Mathf.PI * time) * jumpHeight;
            Vector2 newPos = Vector2.Lerp(startPosition, endPosition, time) + Vector2.up * height;
            rb.MovePosition(newPos);
            yield return null;
        }

        rb.MovePosition(endPosition);
        isJumping = false;
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
