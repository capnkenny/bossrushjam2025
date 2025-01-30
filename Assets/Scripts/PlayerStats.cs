using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] GameObject gameObjectOne;
    [SerializeField] List<Sprite> healthSprites;
    [SerializeField] Image healthRenderer;
    [SerializeField] TextMeshProUGUI chipCount;

    private GameManager gm;

    private int playerHealth = 5;

    void Awake()
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

    void Update()
    {
        if (gm)
        {
            playerHealth = gm.PlayerHealth.Health;
            chipCount.text = gm.GetPlayerCurrency().ToString();
        }
        if(playerHealth == 0)
        {
            //gm.GameOver();
        }
        else
        {
            healthRenderer.sprite = healthSprites[playerHealth - 1];
        }
    }
}
