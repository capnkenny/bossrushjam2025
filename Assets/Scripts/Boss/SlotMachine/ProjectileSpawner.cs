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
    public Vector3 aimPosition = Vector3.zero;
    public int OutputLimit = 8;
    

    private GameObject spawned;
    private float timer = 0f;
    private int spawnCount = 0;

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
        {
            timer = 0;
            spawnCount = 0;
        }
    }

    private void FireProjectile()
    {
        if(projectile && powerup && spawnCount < OutputLimit)
        {
            var pos = transform.position - new Vector3(0,1,0);
            spawned = Instantiate(PowerupEnabled ? powerup : projectile, pos, Quaternion.identity);
            
            var comp = spawned.GetComponent<Projectile>();
            comp.speed = projectileSpeed;
            comp.projectileLifetime = projectileLifetime;
            
            spawned.GetComponent<Rigidbody2D>().AddForce(aimPosition*projectileSpeed*2, ForceMode2D.Impulse);
            spawnCount++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        var calc = transform.position + (aimPosition * 1000);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, calc);
    }


}
