using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            //hurt the player

            //destroy the projectile
            gameObject.GetComponent<Projectile>().Delete();
        }
    }
}
