using UnityEngine;

public class ChipDestroyer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("col: "+c.gameObject.name);

        if(c.gameObject.tag == "Projectile" || c.gameObject.tag == "PlayerProjectile")
        {
            Destroy(c.gameObject);
        }
    }
}
