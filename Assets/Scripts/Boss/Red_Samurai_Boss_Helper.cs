using UnityEngine;
using System.Collections;

public class Red_Samurai_Boss_Helper : MonoBehaviour
{
    private bool isWaiting = true;

    public void StartWaitCoroutine(float waitTime)
    {
        StartCoroutine(WaitBeforeWalkingToBall(waitTime));
    }

    private IEnumerator WaitBeforeWalkingToBall(float waitTime)
    {
        Debug.Log("Waiting for " + waitTime + " seconds");
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        Debug.Log("isWaiting set to false");
    }

    public bool IsWaiting()
    {
        return isWaiting;
    }

    public void ResetWaiting()
    {
        isWaiting = true;
    }
}
