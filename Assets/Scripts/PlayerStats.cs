using UnityEngine;
using UnityEngine.UI;  // For accessing UI Button
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] GameObject gameObjectOne;
    [SerializeField] List<Sprite> healthSprites;
    [SerializeField] Image healthRenderer;
    [SerializeField] TextMeshProUGUI chipCount;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject callDealerOverlay;
    [SerializeField] GameObject callTheDealer;
    [SerializeField] Button returnToGameButton;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Button restartGameButton;

    private GameManager gm;

    private int playerHealth = 5;

    public void Awake()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list == null || list.Length == 0)
        {
            gm = Instantiate(gameObjectOne).GetComponent<EditorGM>();
        }
        else
        {
            gm = list.First();
        }
    }

    public void Start()
    {

        if (callTheDealer != null)
        {
            callTheDealer.SetActive(false);
        }

        if (returnToGameButton != null)
        {
            returnToGameButton.onClick.AddListener(ReturnToGame);
        }

        if (restartGameButton != null)
        {
            restartGameButton.onClick.AddListener(RestartGame);
        }
    }

    public void Update()
    {
        if (gm)
        {
            playerHealth = gm.PlayerHealth.Health;
            chipCount.text = gm.GetPlayerCurrency().ToString();
        }

        if (gm.GetPlayerCurrency() >= 20)
        {
            buttonText.text = "Bet 20 Chips";
            callDealerOverlay.SetActive(true);
        }
        else
        {
            buttonText.text = "No More Bets";
            callDealerOverlay.SetActive(false);
        }

        if (playerHealth == 0)
        {
            gm.Paused = true;
            gameOverScreen.SetActive(true);
        }
        else
        {
            healthRenderer.sprite = healthSprites[playerHealth - 1];
        }

        if (callTheDealer.activeSelf)
        {
            gm.Paused = true;
        }
        
    }

    public void OnInteract(InputValue value)
    {   
        if (value.isPressed && callDealerOverlay.activeSelf && !callTheDealer.activeSelf)
        {
            ShowCallTheDealer();
        }
    }

    public void ShowCallTheDealer()
    {
        if (callTheDealer != null && !callTheDealer.activeSelf)
        {
            callTheDealer.SetActive(true);
        }
    }

    public void ReturnToGame()
    {
        if(gm)
            gm.Paused = false;
        if (callTheDealer != null)
        {
            callTheDealer.SetActive(false);
        }
        
    }

    public void RestartGame()
    {
        gm.ResetToLobby();
    }
}