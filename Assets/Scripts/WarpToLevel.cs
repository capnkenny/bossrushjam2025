using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpToLevel : MonoBehaviour
{
    [SerializeField] public int LevelIndex;
    [SerializeField] public bool ShowTransition = false;
    [SerializeField] public int TransitionDelay;

    private GameManager _manager;

    private delegate void GoToLevelFunction(int level);
    private delegate void TransitionLevelFunction(int level, int delay);

    private GoToLevelFunction GoToLevel;
    private TransitionLevelFunction TransitionToLevel;

    private bool debugTimerActivated = false;
    private float timeToWait = 0;
    private float timePassed = 0f;
    
    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if(list == null || list.Length == 0)
        {
            GoToLevel = (level) => SceneManager.LoadScene(level);
            TransitionToLevel = (level, delay) => { DebugTransition(level, delay);};
        }
        else
        {
            var gm = (GameManager)list.First();
            GoToLevel = gm.LoadLevelImmediate;
            TransitionToLevel = (level, delay) => {gm.LoadLevelWithDelay(level, delay);};
        }
    }

    void Update()
    {
        if(debugTimerActivated)
        {
            timePassed += Time.deltaTime;
            if(timePassed >= timeToWait)
            {
                SceneManager.LoadScene(LevelIndex);
            }
        }
    }

    public void Warp()
    {
        if(ShowTransition)
            TransitionToLevel(LevelIndex, TransitionDelay);
        else
            GoToLevel(LevelIndex);
    }

    public void Warp(int delayInSeconds)
    {
        TransitionToLevel(LevelIndex, delayInSeconds);
    }

    private void DebugTransition(int level, int delayInSeconds)
    {
        debugTimerActivated = true;
        timeToWait = delayInSeconds;
    }

}
