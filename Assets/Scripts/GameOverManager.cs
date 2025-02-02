using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private TextMeshProUGUI toBeContinuedText;

    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }
        toBeContinuedText.gameObject.SetActive(true);
    }

}
