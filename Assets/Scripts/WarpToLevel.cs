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
    
    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if(list == null || list.Length == 0)
        {
            GoToLevel = (level) => SceneManager.LoadScene(level);
            TransitionToLevel = (level, _) => SceneManager.LoadScene(level);
        }
        else
        {
            var gm = (GameManager)list.First();
            GoToLevel = gm.LoadLevelImmediate;
            TransitionToLevel = (level, delay) => {gm.LoadLevelWithDelay(level, delay);};
        }
    }

    public void Warp()
    {
        if(ShowTransition)
            TransitionToLevel(LevelIndex, TransitionDelay);
        else
            GoToLevel(LevelIndex);
    }

}
