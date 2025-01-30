using System.Linq;
using UnityEngine;

public class SlotLevelMgr : MonoBehaviour
{
    public GameManager gm;
    public UnitHealth playerHeath;
    public SlotBossMechanics bossMechs;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bossMechs && bossMechs.dead)
        {
            //Move along to next level.
            gm.LevelOneComplete = true;
        }

        if(playerHeath && playerHeath._currentHealth == 0)
        {
            //game over screen
        }
    }
}
