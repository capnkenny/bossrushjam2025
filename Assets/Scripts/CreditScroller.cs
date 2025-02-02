using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class CreditScroller : MonoBehaviour
{
    public TextMeshProUGUI creditText; 
    public float scrollSpeed = 0.05f; 
    public string filePath = "Assets/Resources/credits.txt";
    public float scrollRate = 10f;

    private string creditContent;
    private bool isScrolling = false;
    private RectTransform creditScreenRectTransform;

    void Start()
    {
        creditScreenRectTransform = GetComponent<RectTransform>();

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

        Vector3 topBoundaryWorldPos = creditScreenRectTransform.TransformPoint(new Vector3(0, creditScreenRectTransform.rect.height / 2, 0));

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
                break;
            }

            yield return null;
        }
    }
}
