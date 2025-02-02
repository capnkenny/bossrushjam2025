using System.Linq;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }

        gameManager.Paused = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Destroy()
    {
        gameManager.Paused = false;
        GameObject.Destroy(gameObject);
    }
}
