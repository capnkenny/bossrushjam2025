using UnityEngine;
public class Red_Samurai_Boss_Behavior : StateMachineBehaviour
{
    public float moveSpeed = 5f;
    public bool shouldResetAndSpin = false;
    // private Transform player;
    private Transform rouletteBall;
    private Rigidbody2D rb;
    private RouletteBall rouletteBallScript;
    private bool hasAttacked = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        rouletteBall = GameObject.FindGameObjectWithTag("Roulette_Ball").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        rouletteBallScript = rouletteBall.GetComponent<RouletteBall>();
        hasAttacked = false;
        shouldResetAndSpin = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!rouletteBallScript.isSpinning && !hasAttacked)
        {
            WalkToRouletteBall(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        // animator.SetBool("isWalking", false);
    }

    void WalkToRouletteBall(Animator animator)
    {
        if (rouletteBall != null)
        {
            animator.SetBool("isWalking", true);
            float step = moveSpeed * Time.deltaTime;

            // Calculate the direction from the boss to the ball
            Vector2 direction = ((Vector2)rouletteBall.position - rb.position).normalized;

            // Determine a target position at a fixed distance from the ball along this direction
            float distanceFromBall = 1.0f; // Adjust this value as needed
            Vector2 targetPosition = (Vector2)rouletteBall.position - direction * distanceFromBall;

            rb.position = Vector2.MoveTowards(rb.position, targetPosition, step);

            if (Vector2.Distance(rb.position, rouletteBall.position) < 1.1f)
            {
                hasAttacked = true;
                shouldResetAndSpin = true;
                animator.SetTrigger("Attack");
                animator.SetBool("isWalking", false);
            }
            
        }
    }

    public void AttackToResetAndSpinBall()
    {
        if (rouletteBallScript != null)
        {
            rouletteBallScript.ResetAndSpinBall();
            shouldResetAndSpin = false;
            hasAttacked = false;
        }
    }
}