using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    public LockableBehaviour Roulette;
    public LockableBehaviour Poker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager)
        {
            if(Roulette)
                Roulette.Locked = gameManager.LevelOneComplete ? false : true;
            if(Poker)
                Poker.Locked = gameManager.LevelTwoComplete ? false : true;
        }
    }
}
