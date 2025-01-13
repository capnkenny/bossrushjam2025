using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] private Button playButton;
    
    private GameManager gm;

    private int LobbyLevelIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if(list == null || list.Length == 0)
        {
            LobbyLevelIndex = 2;
        }
        else
        {
            gm = (GameManager)list.First();
            LobbyLevelIndex = gm.SinglePlayerLobbySceneIndex;
        }

        playButton.onClick.AddListener(() => {TransitionToLobbyScene();});
    }

    void TransitionToLobbyScene()
    {
        if(gm == null)
            SceneManager.LoadScene(LobbyLevelIndex);
        else
            gm.LoadLevelImmediate(LobbyLevelIndex);
    }
}
