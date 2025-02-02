using UnityEngine;

public class ChipDestroyer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c)
    {
        if(c.gameObject.tag == "Projectile" || c.gameObject.tag == "PlayerProjectile")
        {
            Destroy(c.gameObject);
        }
    }
}
