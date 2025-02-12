using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameManager gameManager;
    [SerializeField] private TextMeshProUGUI toBeContinuedText;

    public TextMeshProUGUI thanksText;

    public TextMeshProUGUI creditsText;

    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }
        toBeContinuedText.gameObject.SetActive(true);
        StartCoroutine(ShowThanksForPlaying());
    }

    public IEnumerator ShowThanksForPlaying()
    {
        yield return new WaitForSeconds(3.0f);
        thanksText.gameObject.SetActive(true);
        toBeContinuedText.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.5f);
        thanksText.gameObject.SetActive(false);
        creditsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(6.0f);
        gameManager.LoadLevelImmediate(2);
    }

}
