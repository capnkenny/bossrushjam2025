using UnityEngine;

public class BooleanAnimation : LobbyAction
{
    [SerializeField] private Animator animator;
    [SerializeField] private string animationParameterName;
    [SerializeField] private bool BoolValueToSet;

    public override void PerformAction()
    {
        if(!string.IsNullOrWhiteSpace(ActionName))
            Debug.Log($"Performing action: {ActionName}");
        
        if(animator != null)
            animator.SetBool(animationParameterName, true);
    }
}