using UnityEngine;

public class LoseAnimAction : LobbyAction
{
    [SerializeField] private Animator animator;

    public override void PerformAction()
    {
        if(!string.IsNullOrWhiteSpace(ActionName))
            Debug.Log($"Performing action: {ActionName}");
        
        if(animator != null)
        {
            animator.SetBool("Lose", true);
            animator.SetBool("LeverPulled", false);
        }
    }
}