using System.Linq;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    public WarpToLevel warpSpot;
    public GameManager gm;

void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gm = (GameManager)list.First();
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(gm)
                gm.LevelOneComplete = true;
            warpSpot.Warp();
        }
    }
}
