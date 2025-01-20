using System.Collections;
using UnityEngine;

public class SlotBackground : MonoBehaviour
{

    [Header("Animators")]
    [SerializeField] Animator ReelOneAnimator;
    [SerializeField] Animator ReelTwoAnimator;
    [SerializeField] Animator ReelThreeAnimator;
    [SerializeField] Animator HookOneAnimator;
    [SerializeField] Animator HookTwoAnimator;
    [SerializeField] Animator HookThreeAnimator;

    public const string Lucky7Event = "Lucky7";
    public const string CherryEvent = "Cherry";
    public const string CocoEvent = "Coconut";
    public const string BarEvent = "Bar";
    public const string BellEvent = "Bell";
    public const string DiamondEvent = "Diamond";
    public const string SpinEvent = "Spin";
    
    private const string HookEngageEvent = "Engage";
    private const string HookDisengageEvent = "Engage";

    private const string RollingAnimName = "Rolling";

    private delegate void AnimFunction(int triggerNumber, string eventName);

    void Awake()
    {
        Debug.Log("Starting Slot Machine...");
        ReelOneAnimator.StopPlayback();
        ReelTwoAnimator.StopPlayback();
        ReelThreeAnimator.StopPlayback();
    }

    void Start()
    {
        StartCoroutine(RandomWaitMilliseconds(FirstLaunchReel, 1, ""));
        StartCoroutine(RandomWaitMilliseconds(FirstLaunchReel, 2, ""));
        StartCoroutine(RandomWaitMilliseconds(FirstLaunchReel, 3, ""));
    }

    private IEnumerator RandomWaitMilliseconds(AnimFunction func, int triggerNumber, string eventName)
    {
        float value = Random.Range(0, 500)/1000.0f;
        Debug.Log($"Waiting for {value} seconds");
        yield return new WaitForSeconds(value);
        func(triggerNumber, eventName);
    }

    private void FirstLaunchReel(int triggerNumber, string _)
    {
        switch(triggerNumber)
        {
            case 1:
            {
                Debug.Log("Launching Reel one - first");
                ReelOneAnimator.speed = Random.Range(0.75f, 1.25f);
                ReelOneAnimator.Play(RollingAnimName);
                break;
            }
            case 2:
            {
                Debug.Log("Launching Reel two - first");
                ReelTwoAnimator.speed = Random.Range(0.75f, 1.25f);
                ReelTwoAnimator.Play(RollingAnimName);
                break;
            }
            case 3:
            default:
            {
                Debug.Log("Launching Reel three - first");
                ReelThreeAnimator.speed = Random.Range(0.75f, 1.25f);
                ReelThreeAnimator.Play(RollingAnimName);
                break;
            }
        }
    }

    public void SetReel(int triggerNumber, string evName)
    {
        switch(triggerNumber)
        {
            case 1:
            {
                Debug.Log($"Triggering Reel one - {evName}");
                ReelOneAnimator.SetTrigger(evName);
                HookOneAnimator.SetTrigger(HookEngageEvent);
                break;
            }
            case 2:
            {
                Debug.Log($"Triggering Reel two - {evName}");
                ReelTwoAnimator.SetTrigger(evName);
                HookTwoAnimator.SetTrigger(HookEngageEvent);
                break;
            }
            case 3:
            default:
            {
                Debug.Log($"Triggering Reel three - {evName}");
                ReelThreeAnimator.SetTrigger(evName);
                HookThreeAnimator.SetTrigger(HookEngageEvent);
                break;
            }
        }
    }

    public void ResetReels()
    {
        StartCoroutine(RandomWaitMilliseconds(LaunchReel, 1, ""));
        StartCoroutine(RandomWaitMilliseconds(LaunchReel, 2, ""));
        StartCoroutine(RandomWaitMilliseconds(LaunchReel, 3, ""));
    }

    private void LaunchReel(int triggerNumber, string _)
    {
        switch(triggerNumber)
        {
            case 1:
            {
                Debug.Log($"Spinning Reel one");
                HookOneAnimator.SetTrigger(HookDisengageEvent);
                ReelOneAnimator.SetTrigger(SpinEvent);
                break;
            }
            case 2:
            {
                Debug.Log($"Spinning Reel two");
                HookTwoAnimator.SetTrigger(HookDisengageEvent);
                ReelTwoAnimator.SetTrigger(SpinEvent);
                break;
            }
            case 3:
            default:
            {
                Debug.Log($"Spinning Reel three");
                HookThreeAnimator.SetTrigger(HookDisengageEvent);
                ReelThreeAnimator.SetTrigger(SpinEvent);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        #if DEBUG
        if(Input.anyKeyDown)
        {
            SetReel(1, CherryEvent);
        }
        #endif
    }
}
