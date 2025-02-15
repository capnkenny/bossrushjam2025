using UnityEngine;

public class PlayerSpawnerFB : MonoBehaviour
{
    [Header("Projectile Vars")]
    public GameObject Projectile;

    public float projectileLifetime = 1f;
    public float projectileSpeed = 1f;
    public AudioClip projectileClip;

    [Header("Power Up One")]
    public GameObject PowerUpOne;
    public float projectileLifetimePUO = 1f;
    public float projectileSpeedPUO = 1f;
    public int projectileDamagePUO = 1;
    public AudioClip projectileClipPUO;

    [Header("Power Up Two")]
    public GameObject PowerUpTwo;
    public float projectileLifetimePUT = 1f;
    public float projectileSpeedPUT = 1f;
    public int projectileDamagePUT = 1;
    public AudioClip projectileClipPUT;

    [Header("Power Up Three")]
    public GameObject PowerUpThree;
    public float projectileLifetimePUTH = 1f;
    public float projectileSpeedPUTH = 1f;
    public int projectileDamagePUTH = 1;
    public AudioClip projectileClipPUTH;
    
    [Header("Spawner Vars")]
    public bool FiringEnabled = false;
    public AudioSource audioSource;
    public int PowerUpMode = 0;
    public float firingRate = 1f;
    public float scaleRate = 2.0f;
    private Vector3 aimPosition = Vector3.zero;
    public int OutputLimit = 8;
    public bool Frozen = false;
    public PlayerMovement playerMovement;


    private GameObject spawned;
    private float timer = 0f;

    void Update()
    {
        aimPosition = playerMovement.GetDirection();
        if(FiringEnabled && !Frozen)
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
        }
    }

    private void FireProjectile()
    {
        if(Projectile)
        {
            var pos = transform.position - new Vector3(0,1,0);
            switch(PowerUpMode)
            {
                case 1:
                    spawned = Instantiate(PowerUpOne, pos, Quaternion.identity);
                    audioSource.PlayOneShot(projectileClipPUO);
                    break;
                case 2:
                    spawned = Instantiate(PowerUpTwo, pos, Quaternion.identity);
                    audioSource.PlayOneShot(projectileClipPUT);
                    break;
                case 3:
                    spawned = Instantiate(PowerUpThree, pos, Quaternion.identity);
                    audioSource.PlayOneShot(projectileClipPUTH);
                    break;
                default:
                    spawned = Instantiate(Projectile, pos, Quaternion.identity);
                    audioSource.PlayOneShot(projectileClip);
                    break;
            }
            spawned.transform.localScale = Vector3.one * scaleRate;
            
            var comp = spawned.GetComponent<Projectile>();
            switch(PowerUpMode)
            {
                case 1:
                    comp.speed = projectileSpeedPUO;
                    comp.projectileLifetime = projectileLifetimePUO;
                    comp.DamageValue = projectileDamagePUO;
                    break;
                case 2:
                    comp.speed = projectileSpeedPUT;
                    comp.projectileLifetime = projectileLifetimePUT;
                    comp.DamageValue = projectileDamagePUT;
                    break;
                case 3:
                    comp.speed = projectileSpeedPUTH;
                    comp.projectileLifetime = projectileLifetimePUTH;
                    comp.DamageValue = projectileDamagePUTH;
                    break;
                default:
                    comp.speed = projectileSpeed;
                    comp.projectileLifetime = projectileLifetime;
                    comp.DamageValue = 1;
                    break;
            }

            var variance = new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);

            spawned.GetComponent<Rigidbody2D>().AddForce((aimPosition*comp.speed*2*scaleRate)+variance, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        var calc = transform.right;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.right, calc);
    }


}
