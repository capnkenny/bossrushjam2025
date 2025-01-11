using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsRunning", false);

        if(context.canceled)
        {
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastInputX", movement.x);
            animator.SetFloat("LastInputY", movement.y);
        }
        movement = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);

        // Main Character Specific Movement
        animator.SetFloat("x", movement.x);
        animator.SetFloat("y", movement.y);
    }
}
