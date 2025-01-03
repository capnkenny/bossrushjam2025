using UnityEngine;

public class Preloader : MonoBehaviour
{
    [SerializeField] private GameManager _manager;

    
    void Start()
    {
        _manager.LoadLevelImmediate(_manager.TitleScreenSceneIndex);
    }
}
