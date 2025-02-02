using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private GameObject creditScreen;

    public TMPro.TextMeshProUGUI creditText;
    public float scrollSpeed = 0.05f;
    public string filePath = "Assets/GameCredits.txt";
    public float scrollRate = 10f;

    private GameManager gm;
    private string creditContent;
    private bool isScrolling = false;
    private RectTransform creditScreenRectTransform;
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
        creditsButton.onClick.AddListener(OnCreditButtonClick);

        creditScreenRectTransform = creditText.GetComponentInParent<RectTransform>();

    }

    void TransitionToLobbyScene()
    {
        if(gm == null)
            SceneManager.LoadScene(LobbyLevelIndex);
        else
            gm.LoadLevelImmediate(LobbyLevelIndex);
    }

    void OnCreditButtonClick()
    {
        creditScreen.SetActive(true);
        StartCoroutine(ReadAndScrollCredits());
    }

    IEnumerator ReadAndScrollCredits()
    {

        if (File.Exists(filePath))
        {
            creditContent = File.ReadAllText(filePath);
        }
        else
        {
            Debug.LogError("Credit file not found at " + filePath);
            yield break;
        }

        creditText.text = "";

        foreach (char letter in creditContent)
        {
            creditText.text += letter;
            yield return new WaitForSeconds(scrollSpeed);
        }

        isScrolling = true;

        creditScreenRectTransform = creditScreen.GetComponent<RectTransform>();

        Vector3 topBoundaryLocalPos = new Vector3(0, creditScreenRectTransform.rect.height / 2, 0);

        Vector3 topBoundaryWorldPos = creditScreenRectTransform.TransformPoint(topBoundaryLocalPos);

        Debug.Log("Top Boundary World Position: " + topBoundaryWorldPos);

        while (isScrolling)
        {
            creditText.transform.position += Vector3.up * scrollRate * Time.deltaTime;

            Debug.Log("Credit Text World Position: " + creditText.transform.position);

            if (creditText.transform.position.y >= topBoundaryWorldPos.y)
            {
                if (creditText.text.Length < creditContent.Length)
                {
                    yield return null;
                    continue;
                }
            }

            if (creditText.text.Length == creditContent.Length && creditText.transform.position.y >= topBoundaryWorldPos.y)
            {
                creditText.gameObject.SetActive(false);

                creditScreen.SetActive(false);

                break;
            }

            yield return null;
        }
    }
}
