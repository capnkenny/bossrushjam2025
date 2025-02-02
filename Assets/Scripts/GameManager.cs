using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Scene Indices")]

    [SerializeField] public int TitleScreenSceneIndex;
    [SerializeField] public int SinglePlayerLobbySceneIndex;
    [SerializeField] public int RouletteCutsceneIndex;
    [SerializeField] public int RouletteTableSceneIndex;
    [SerializeField] public int SlotMachineCutsceneIndex;
    [SerializeField] public int SlotMachineSceneIndex;
    [SerializeField] public int CrapsTableCutsceneIndex;
    [SerializeField] public int CrapsTableSceneIndex;

    [Header("Game Mechanics")]

    [SerializeField] public float InitialDifficulty;
    [SerializeField, Range(1f, 10f)] public float DifficultyScale;
    [SerializeField] public UnitHealth PlayerHealth;
    [SerializeField] private int PlayerCurrency;
    [SerializeField] public int PlayerPowerUpMode;
    public float PlayerSpeedRate = 1.0f;

    public bool LevelOneComplete = false;
    public bool LevelTwoComplete = false;
    public bool LevelThreeComplete = false;

    private float _defaultTransitionTime = 3.0f;

    private double timePlayed;

    public bool Paused = false;
    public bool InGame = false;

    // Called when MonoBehaviour is first instantiated before Start()
    void Awake()
    {
        //Don't get used to this pls.
        DontDestroyOnLoad(gameObject);    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(InGame)
        {
            timePlayed += Time.deltaTime;
        }
    }

    public IEnumerator LoadLevelWithDelay(int index, int delayInSeconds)
    {
        Debug.Log($"Scene requested - id: {index}");
        //TODO: Do we need a transition?
        yield return new WaitForSeconds(delayInSeconds);

        SceneManager.LoadScene(index);
    }

    public void LoadLevelImmediate(int index)
    {
        Debug.Log($"Scene requested immediately - id: {index}");
        
        SceneManager.LoadScene(index);
    }

    public int GetPlayerCurrency()
    {
        return PlayerCurrency > 999 ? 999 : PlayerCurrency;
    }

    public void AddToCurrency(int currency)
    {
        PlayerCurrency += currency;
    }

    public void ResetToLobby()
    {
        PlayerPowerUpMode = 0;
        PlayerHealth._currentHealth = 5;
        PlayerHealth._currentMaxHealth = 5;
        LoadLevelImmediate(2);
    }

}
