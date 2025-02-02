using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Player Properties")]
    [SerializeField] public float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private Animator animator;
    [SerializeField] private float animationSmoothingValue;
    [SerializeField] public bool BattleMode;
    [SerializeField] private bool RestrictXMovement;
    [SerializeField] private bool RestrictYMovement;
    
    [Header("FOR VISIBILITY ONLY")]
    [SerializeField]private Vector2 movement;
    [SerializeField]private Vector2 previousMovement;
    [SerializeField]private bool isRunning;
    [SerializeField]private float defaultMoveSpeed = 5f;
    private float idleValue = 0.1f;
    private float trueSpeed = 0.0f;
    private float animationX = 0;
    private float animationY = 0;
    private Transform rouletteBall;
    private RouletteBall rouletteBallScript;

    public bool rouletteMode = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }

        movement = new Vector2(0,0);
        if (animationSmoothingValue <= 0)
            animationSmoothingValue = 2;
        if (moveSpeed <= 0)
            moveSpeed = 2f;

        int playerLayer = LayerMask.NameToLayer("Player");
        int rouletteLayer = LayerMask.NameToLayer("Roulette");
        Physics2D.IgnoreLayerCollision(playerLayer, rouletteLayer, true);

        GameObject rouletteBallObject = GameObject.FindGameObjectWithTag("Roulette_Ball");
        if (rouletteBallObject != null)
        {
            rouletteBall = rouletteBallObject.transform;
            rouletteBallScript = rouletteBall.GetComponent<RouletteBall>();

            if (rouletteBallScript == null)
            {
                Debug.LogWarning("RouletteBall script not found on the Roulette_Ball GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("Roulette_Ball GameObject not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float halfSpeed = moveSpeed * 0.75f;
        var trueMovement = movement;
        if (!gameManager.Paused)
        {

            trueSpeed += isRunning ? moveSpeed * Time.deltaTime : halfSpeed * Time.deltaTime;
            trueSpeed *= gameManager.PlayerSpeedRate;

            if (isRunning && trueSpeed >= moveSpeed)
            {
                trueSpeed = moveSpeed;
            }
            else if (!isRunning && trueSpeed >= halfSpeed)
                trueSpeed = halfSpeed;
            if (RestrictXMovement)
                trueMovement.x = 0;
            if (RestrictYMovement)
                trueMovement.y = 0;

            rb.linearVelocity = trueMovement * trueSpeed;
            SetAnimatorMovement(movement, previousMovement);
        }
        rb.linearVelocity = trueMovement * trueSpeed;
        SetAnimatorMovement(movement, previousMovement);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && rouletteMode == true)
        {
            animator.SetTrigger("Attack");

            if (rouletteBall != null && rouletteBallScript != null)
            {
                if (Vector2.Distance(rb.position, rouletteBall.position) < 2f)
                {
                    rouletteBallScript.ResetAndSpinBall(true);
                }
            }
            else
            {
                Debug.LogWarning("rouletteBall or rouletteBallScript is null.");
            }
        }
    }

    public void OnSprint(InputValue _)
    {
        if (!gameManager.Paused)
        {
            //Creates a "toggle" effect for running
            if (!BattleMode)
                isRunning = !isRunning;
        }
    }

    public void OnMove(InputValue value)
    {
        if (!gameManager.Paused)
        {
            previousMovement = movement;
            movement = value.Get<Vector2>();
        }
    }

    public void IncreaseMovementSpeed(int amount)
    {
        moveSpeed += amount;
        Debug.Log("Player movement speed increased");
    }

    public void DecreaseMovementSpeed(int amount)
    {
        moveSpeed = Mathf.Max(1f, moveSpeed - amount); // Decrease movement speed by 1, but not below 1
        Debug.Log("Player movement speed decreased");
    }
    public void ResetMoveSpeed()
    {
        moveSpeed = defaultMoveSpeed;
        Debug.Log("Player movement speed reset");
    }
    public void ResetAttackTrigger()
    {
        animator.ResetTrigger("Attack");
    }

    private void SetAnimatorMovement(Vector2 movementValue, Vector2 previousValue)
    {
        float deltaSmoothing = Time.deltaTime * animationSmoothingValue;

        //Handle cases where we stopped moving
        if (movementValue.x == 0 && movementValue.y == 0)
        {
            if (previousValue.x > 0)
            {
                animationX = idleValue;
            }
            else if (previousValue.x < 0)
            {
                animationX = -idleValue;
            }
            else
                animationX = 0;

            if (previousValue.y > 0)
            {
                animationY = idleValue;
            }
            else if (previousValue.y < 0)
            {
                animationY = -idleValue;
            }
            else
                animationY = 0;
        }
        else
        {
            //Handle walking + running blends
            float multiplier = isRunning ? 1 : 0.3f;

            float horiz = movementValue.x * multiplier;
            float vert = movementValue.y * multiplier;

            if (Mathf.Abs(animationY - vert) < 0.1f)
                animationY = vert;
            if (Mathf.Abs(animationX - horiz) < 0.1f)
                animationX = horiz;

            if (animationX < horiz)
                animationX += deltaSmoothing;
            else if (animationX > horiz)
                animationX -= deltaSmoothing;

            if (animationY < vert)
                animationY += deltaSmoothing;
            else if (animationY > vert)
                animationY -= deltaSmoothing;
        }

        animator.SetFloat("InputX", animationX);
        animator.SetFloat("InputY", animationY);
    }

    public Vector2 GetDirection()
    {
        return (movement-previousMovement).normalized;
    }
}
