using UnityEngine;

public class Reel : MonoBehaviour
{
    public ProjectileSpawner EmitterLeft;
    public ProjectileSpawner EmitterCenter;
    public ProjectileSpawner EmitterRight;

    [Header("Coconut Properties")]
    public float cocoFiringRate = 0.250f;
    public float cocoLifetime = 5f;
    public float cocoSpeed = 20f;

    public void Reset()
    {
        if(EmitterLeft)
        {
            EmitterLeft.FiringEnabled = false;
            EmitterLeft.PowerupEnabled = false;
        }
        if(EmitterCenter)
        {
            EmitterCenter.FiringEnabled = false;
            EmitterCenter.PowerupEnabled = false;
        }
        if(EmitterRight)
        {
            EmitterRight.FiringEnabled = false;
            EmitterRight.PowerupEnabled = false;
        }
    }

    public void FireCoconut()
    {
        if(EmitterLeft)
        {
            EmitterLeft.firingRate = cocoFiringRate;
            EmitterLeft.projectileLifetime = cocoLifetime;
            EmitterLeft.projectileSpeed = 20f;
            EmitterLeft.FiringEnabled = true;
        }
        if(EmitterCenter)
        {
            EmitterCenter.firingRate = cocoFiringRate;
            EmitterCenter.projectileLifetime = cocoLifetime;
            EmitterCenter.projectileSpeed = 20f;
            EmitterCenter.FiringEnabled = true;
        }
        if(EmitterRight)
        {
            EmitterRight.firingRate = cocoFiringRate;
            EmitterRight.projectileLifetime = cocoLifetime;
            EmitterRight.projectileSpeed = 20f;
            EmitterRight.FiringEnabled = true;
        }
    }
}
