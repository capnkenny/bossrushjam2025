using System.Linq;
using UnityEngine;

public class SlotLevelMgr : MonoBehaviour
{
    public GameManager gm;
    public UnitHealth playerHealth;
    public SlotBossMechanics bossMechs;
    public Animator ExitLevelAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gm = list.First();
        }

        if(gm)
        {
            gm.InGame = true;
            playerHealth = gm.PlayerHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bossMechs && bossMechs.dead)
        {
            gm.LevelOneComplete = true;
            ExitLevelAnimator.SetTrigger("Appear");
        }
    }
}
