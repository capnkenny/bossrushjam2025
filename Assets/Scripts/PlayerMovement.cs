using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Properties")]
    [SerializeField] public float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private Animator animator;
    [SerializeField] private float animationSmoothingValue;
    [SerializeField] private bool BattleMode;
    [SerializeField] private bool RestrictXMovement;
    [SerializeField] private bool RestrictYMovement;
    
    [Header("FOR VISIBILITY ONLY")]
    [SerializeField]private Vector2 movement;
    [SerializeField]private Vector2 previousMovement;
    [SerializeField]private bool isRunning;

    [Header("Wheel Properties")]
    [SerializeField] private Transform wheelCenter;
    [SerializeField] private float wheelRadius;

    private float idleValue = 0.1f;
    private float trueSpeed = 0.0f;
    private float animationX = 0;
    private float animationY = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = new Vector2(0,0);
        if (animationSmoothingValue <= 0)
            animationSmoothingValue = 2;
        if (moveSpeed <= 0)
            moveSpeed = 2f;

        int playerLayer = LayerMask.NameToLayer("Player");
        int rouletteLayer = LayerMask.NameToLayer("Roulette");
        Physics2D.IgnoreLayerCollision(playerLayer, rouletteLayer, true);
    }

    // Update is called once per frame
    void Update()
    {
         float halfSpeed = moveSpeed * 0.75f;

        trueSpeed += isRunning ? moveSpeed * Time.deltaTime : halfSpeed * Time.deltaTime;

        if (isRunning && trueSpeed >= moveSpeed)
        {
            trueSpeed = moveSpeed;
        }
        else if (!isRunning && trueSpeed >= halfSpeed)
            trueSpeed = halfSpeed;

        var trueMovement = movement;
        if(RestrictXMovement)
            trueMovement.x = 0;
        if(RestrictYMovement)
            trueMovement.y = 0;

        rb.linearVelocity = trueMovement * trueSpeed;
        SetAnimatorMovement(movement, previousMovement);

        ConstrainPlayerWithinWheel();
    }

    public void OnSprint(InputValue _)
    {
        //Creates a "toggle" effect for running
        if(!BattleMode)
            isRunning = !isRunning;
    }

    public void OnMove(InputValue value)
    {
        previousMovement = movement;
        movement = value.Get<Vector2>();
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

    private void ConstrainPlayerWithinWheel()
    {
        Vector2 playerPosition = rb.position;
        Vector2 wheelCenterPosition = wheelCenter.position;
        Vector2 directionFromCenter = playerPosition - wheelCenterPosition;

        if (directionFromCenter.magnitude > wheelRadius)
        {
            directionFromCenter = Vector2.ClampMagnitude(directionFromCenter, wheelRadius);
            rb.position = wheelCenterPosition + directionFromCenter;
        }
    }
}
