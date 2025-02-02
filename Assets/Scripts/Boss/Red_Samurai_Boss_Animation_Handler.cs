using UnityEngine;

public class Red_Samurai_Boss_Animation_Handler : MonoBehaviour
{
    private Red_Samurai_Boss_Behavior bossBehavior;

    void Start()
    {
        bossBehavior = GetComponent<Animator>().GetBehaviour<Red_Samurai_Boss_Behavior>();
    }

    // This method will be called by the animation event
    public void ResetAndSpinBall()
    {
        if (bossBehavior != null && bossBehavior.shouldResetAndSpin)
        {
            bossBehavior.AttackToResetAndSpinBall();
        }
    }
}
