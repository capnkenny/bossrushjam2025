using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    public WarpToLevel warpSpot;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            warpSpot.Warp();
        }
    }
}
