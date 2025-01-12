using UnityEngine;

public class SlotMachineLobbyLogic : MonoBehaviour
{   
    [SerializeField] private Animator animatorToWatch;
    [SerializeField] public string AnimatorTurnedOnTrigger = "TurnedOn";
    [SerializeField] public string AnimatorLeverPulledTrigger = "LeverPulled";
    [SerializeField] public string AnimatorLoseTrigger = "Lose";

    void Update()
    {
        if(animatorToWatch != null)
        {
            var anim = animatorToWatch.GetCurrentAnimatorClipInfo(0);
            if(anim != null && anim.Length > 0 && anim[0].clip.name == "SMPullLever")
            {
                var state = animatorToWatch.GetCurrentAnimatorStateInfo(0);
                if(state.normalizedTime >= state.length)
                {
                    animatorToWatch.SetBool("LeverPulled", true);
                    animatorToWatch.SetBool("TurnedOn", false);
                }
            }
        }
    }
}
