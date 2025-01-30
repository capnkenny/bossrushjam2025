using UnityEngine;
using System.Collections;
using System.Threading.Tasks;

public class RouletteBall : MonoBehaviour
{
    public float initialForce = 10f;
    public float minForce = 10f;
    public float maxForce = 40f;
    public float initialFriction = 1f;
    public float finalFriction = 0.98f;
    public float wheelRadius = 5f;
    public float torqueAmount = 5f;
    public float elapsedTime = 0f;
    public float spinTime = 5f;
    public Transform rouletteWheel;
    public int failedAttempts = 0;
    public const int maxFailedAttempts = 3;
    public bool isSpinning = true;
    private Rigidbody2D rb;
    private Collider2D ballCollider;
    private Collider2D[] numberColliders;
    private bool moveToGreen = false;
    private Vector2 greenPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();

        numberColliders = rouletteWheel.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D numberCollider in numberColliders)
        {
            Physics2D.IgnoreCollision(ballCollider, numberCollider, true);
        }

        Vector2 initialDirection = new Vector2(0,1).normalized;
        rb.AddForce(initialDirection * initialForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (isSpinning)
        {
            rb.AddTorque(torqueAmount);
            elapsedTime += Time.fixedDeltaTime;

            float friction = elapsedTime > 5f ? finalFriction : initialFriction;

            // Apply friction to the ball
            rb.linearVelocity *= friction;

            // Stop the ball if it's moving too slow
            if (rb.linearVelocity.magnitude < 0.1f)
            {
                isSpinning = false;
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;

                SnapToClosestNumber();

                foreach (Collider2D numberCollider in numberColliders)
                {
                    Physics2D.IgnoreCollision(ballCollider, numberCollider, false);
                }

            }

            // Keep the ball within the wheel
            Vector2 wheelCenter = rouletteWheel.position;
            Vector2 ballPosition = rb.position;
            Vector2 directionFromCenter = ballPosition - wheelCenter;
            if (directionFromCenter.magnitude > wheelRadius)
            {
                directionFromCenter = directionFromCenter.normalized * wheelRadius;
                rb.position = wheelCenter + directionFromCenter;
            }

            // Adjust the ball's velocity to follow a circular path
            Vector2 tangent = new Vector2(-directionFromCenter.y, directionFromCenter.x).normalized;
            rb.linearVelocity = tangent * rb.linearVelocity.magnitude;
        }
        else
        {
            // Ensure the ball stays within the wheel when it stops
            Vector2 wheelCenter = rouletteWheel.position;
            Vector2 ballPosition = rb.position;
            Vector2 directionFromCenter = ballPosition - wheelCenter;
            if (directionFromCenter.magnitude > wheelRadius)
            {
                directionFromCenter = directionFromCenter.normalized * wheelRadius;
                rb.position = wheelCenter + directionFromCenter;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetAndSpinBall();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveBallToGreen();
        }
        if (moveToGreen)
        {
            Vector2 ballPosition = rb.position;
            Vector2 newPosition = Vector2.MoveTowards(ballPosition, greenPosition, initialForce * Time.deltaTime);
            rb.MovePosition(newPosition);

            if (Vector2.Distance(ballPosition, greenPosition) < 0.1f) // Adjust the threshold as needed
            {
                // Ensure the ball stops exactly at the green position
                rb.position = greenPosition;
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.isKinematic = false;
                moveToGreen = false;
                isSpinning = false;
            }
        }
    }

    public void ResetAndSpinBall()
    {
        Vector2 wheelCenter = rouletteWheel.position;
        Vector2 startPosition = wheelCenter + new Vector2(0, wheelRadius);
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        elapsedTime = 0f;
        isSpinning = true;

        // Calculate the tangent direction at the starting position
        Vector2 directionFromCenter = rb.position - wheelCenter;
        Vector2 tangent = new Vector2(-directionFromCenter.y, directionFromCenter.x).normalized;

        ballCollider = GetComponent<Collider2D>();

        numberColliders = rouletteWheel.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D numberCollider in numberColliders)
        {
            Physics2D.IgnoreCollision(ballCollider, numberCollider, true);
        }

        rb.isKinematic = false;

        initialForce = Random.Range(minForce, maxForce);
        rb.AddForce(tangent * initialForce, ForceMode2D.Impulse);
    }

    void SnapToClosestNumber()
    {
        float closestDistance = float.MaxValue;
        Collider2D closestCollider = null;
        foreach (Collider2D numberCollider in numberColliders)
        {
            float distance = Vector2.Distance(rb.position, numberCollider.bounds.center);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = numberCollider;
            }
        }

        if (closestCollider != null)
        {
            ballCollider.enabled = false;
            rb.position = closestCollider.bounds.center;
            ballCollider.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isSpinning)
        {
            string tag = collision.gameObject.tag;
            string[] parts = tag.Split(' ');
            if (parts.Length == 3)
            {
                string number = parts[1];
                string color = parts[2];
                Debug.Log($"Number: {number}, Color: {color}");

                if (number == "0" && color == "Green")
                {
                    failedAttempts = 0;
                }
                else
                {
                    failedAttempts++;
                    if (failedAttempts >= maxFailedAttempts)
                    {
                        MoveBallToGreen();
                        failedAttempts = 0;
                    }
                }

                // Make the ball kinematic to prevent it from being pushed out
                rb.isKinematic = true;
            }
        }
    }

    void MoveBallToGreen()
    {
        rb.isKinematic = true;

        // Find the position of the "Roulette 0 Green" GameObject
        GameObject greenSlot = GameObject.FindGameObjectWithTag("Roulette 0 Green");
        if (greenSlot != null)
        {
            isSpinning = true;
            moveToGreen = true;
            greenPosition = greenSlot.transform.position;
            Vector2 ballPosition = rb.position;

            // Calculate the direction to the green slot
            Vector2 directionToGreen = (greenPosition - ballPosition).normalized;

            // Calculate the new position for the ball
            Vector2 newPosition = Vector2.MoveTowards(ballPosition, greenPosition, initialForce * Time.deltaTime);

            // Move the ball to the new position
            rb.MovePosition(newPosition);
        }
    }
}
