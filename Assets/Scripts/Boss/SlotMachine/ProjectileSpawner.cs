using System.Threading;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [Header("Projectile Vars")]
    public GameObject projectile;
    public GameObject powerup;
    public float projectileLifetime = 1f;
    public float projectileSpeed = 1f;
    
    [Header("Spawner Vars")]
    public bool FiringEnabled = false;
    public bool PowerupEnabled = false;
    public float firingRate = 1f;
    

    private GameObject spawned;
    private float timer = 0f;

    void Update()
    {
        if(FiringEnabled)
        {
            timer += Time.deltaTime;
            if(timer >= firingRate)
            {
                FireProjectile();
                timer = 0;
            }
        }
        else if(timer != 0)
            timer = 0;
    }

    private void FireProjectile()
    {
        if(projectile && powerup)
        {
            spawned = Instantiate(PowerupEnabled ? powerup : projectile, transform.position, Quaternion.identity);
            var comp = spawned.GetComponent<Projectile>();
            comp.speed = projectileSpeed;
            comp.projectileLifetime = projectileLifetime;
            comp.transform.rotation = transform.rotation;
        }
    }


}
