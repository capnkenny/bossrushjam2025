using UnityEngine;

public class RouletteRoom : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private Transform rouletteBall;
    private RouletteBall rouletteBallScript;
    private bool isBallSpinning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rouletteBall = GameObject.FindGameObjectWithTag("Roulette_Ball").transform;
        rouletteBallScript = rouletteBall.GetComponent<RouletteBall>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rouletteBallScript.isSpinning)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

        // Method to start the ball spin and room rotation
    public void StartBallSpin()
    {
        isBallSpinning = true;
        // Add logic to start the ball spinning if needed
    }

    // Method to stop the ball spin and room rotation
    public void StopBallSpin()
    {
        isBallSpinning = false;
        // Add logic to stop the ball spinning if needed
    }
}
