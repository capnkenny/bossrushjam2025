using System.Collections;
using UnityEngine;

public class Reel : MonoBehaviour
{
    public ProjectileSpawner EmitterLeft;
    public ProjectileSpawner EmitterCenter;
    public ProjectileSpawner EmitterRight;
    public ParticleSystem particles;
    public ParticleSystem particles2;
    public AudioSource source;
    public AudioClip explosionClip;
    public AudioClip dispenseClip;
    public float dispenseWaitInSeconds = 0.1f;

    [Header("Coconut Properties")]
    public float cocoFiringRate = 0.250f;
    public float cocoLifetime = 5f;
    public float cocoSpeed = 20f;

    [Header("Bar Properties")]
    public float barFiringRate = 0.100f;
    public float barLifetime = 5f;
    public float barSpeed = 20f;

    [Header("Diamond Properties")]
    public float diaFiringRate = 0.100f;
    public float diaLifetime = 5f;
    public float diaSpeed = 20f;

    [Header("Cherry Properties")]
    public float cherryFiringRate = 0.100f;
    public float cherryLifetime = 5f;
    public float cherrySpeed = 20f;

    [Header("Bell Properties")]
    public float bellFiringRate = 0.100f;
    public float bellLifetime = 5f;
    public float bellSpeed = 20f;

    [Header("Lucky 7 Properties")]
    public float sevenFiringRate = 0.100f;
    public float sevenLifetime = 5f;
    public float sevenSpeed = 20f;


    public void ResetAndFreeze()
    {
        Reset();
        if(EmitterLeft)
        {
            EmitterLeft.Frozen = true;
        }
        if(EmitterCenter)
        {
            EmitterCenter.Frozen = true;
        }
        if(EmitterRight)
        {
            EmitterRight.Frozen = true;
        }
        if(particles && particles2)
        {
            particles.Play();
            particles2.Play();
            source.PlayOneShot(explosionClip);
        }
    }

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
            EmitterLeft.projectileSpeed = cocoSpeed;
            EmitterLeft.FiringEnabled = true;
        }
        if(EmitterCenter)
        {
            EmitterCenter.firingRate = cocoFiringRate;
            EmitterCenter.projectileLifetime = cocoLifetime;
            EmitterCenter.projectileSpeed = cocoSpeed;
            EmitterCenter.FiringEnabled = true;
            StartCoroutine(PlayDispenseSound());
        }
        if(EmitterRight)
        {
            EmitterRight.firingRate = cocoFiringRate;
            EmitterRight.projectileLifetime = cocoLifetime;
            EmitterRight.projectileSpeed = cocoSpeed;
            EmitterRight.FiringEnabled = true;
        }
    }

    public void FireBar()
    {
        if(EmitterCenter)
        {
            EmitterCenter.firingRate = barFiringRate;
            EmitterCenter.projectileLifetime = barLifetime;
            EmitterCenter.projectileSpeed = barSpeed;
            EmitterCenter.FiringEnabled = true;
            StartCoroutine(PlayDispenseSound());
        }
    }

    public void FireDiamond()
    {
        if(EmitterLeft)
        {
            EmitterLeft.firingRate = diaFiringRate;
            EmitterLeft.projectileLifetime = diaFiringRate;
            EmitterLeft.projectileSpeed = diaSpeed;
            EmitterLeft.PowerupEnabled = true;
            EmitterLeft.FiringEnabled = true;
            StartCoroutine(PlayDispenseSound());
        }
        if(EmitterRight)
        {
            EmitterRight.firingRate = diaFiringRate;
            EmitterRight.projectileLifetime = diaLifetime;
            EmitterRight.projectileSpeed = diaSpeed;
            EmitterRight.PowerupEnabled = true;
            EmitterRight.FiringEnabled = true;
        }
    }

    public void FireBell()
    {
        if(EmitterLeft)
        {
            EmitterLeft.firingRate = bellFiringRate;
            EmitterLeft.projectileLifetime = bellLifetime;
            EmitterLeft.projectileSpeed = bellSpeed;
            EmitterLeft.FiringEnabled = true;
            StartCoroutine(PlayDispenseSound());
        }
        if(EmitterCenter)
        {
            EmitterCenter.firingRate = bellFiringRate;
            EmitterCenter.projectileLifetime = bellLifetime;
            EmitterCenter.projectileSpeed = bellSpeed;
            EmitterCenter.PowerupEnabled = true;
            EmitterCenter.FiringEnabled = true;
        }
        if(EmitterRight)
        {
            EmitterRight.firingRate = bellFiringRate;
            EmitterRight.projectileLifetime = bellLifetime;
            EmitterRight.projectileSpeed = bellSpeed;
            EmitterRight.FiringEnabled = true;
        }
    }

    public void FireCherry()
    {
        if(EmitterLeft)
        {
            EmitterLeft.firingRate = cherryFiringRate;
            EmitterLeft.projectileLifetime = cherryLifetime;
            EmitterLeft.projectileSpeed = cherrySpeed;
            EmitterCenter.PowerupEnabled = true;
            EmitterLeft.FiringEnabled = true;
        }
        if(EmitterCenter)
        {
            EmitterCenter.firingRate = cherryFiringRate;
            EmitterCenter.projectileLifetime = cherryLifetime;
            EmitterCenter.projectileSpeed = cherrySpeed;
            EmitterCenter.PowerupEnabled = true;
            EmitterCenter.FiringEnabled = true;
            StartCoroutine(PlayDispenseSound());
        }
        if(EmitterRight)
        {
            EmitterRight.firingRate = cherryFiringRate;
            EmitterRight.projectileLifetime = cherryLifetime;
            EmitterRight.projectileSpeed = cherrySpeed;
            EmitterCenter.PowerupEnabled = true;
            EmitterRight.FiringEnabled = true;
        }
    }

    public void FireLucky7()
    {
        if(EmitterLeft)
        {
            EmitterLeft.firingRate = sevenFiringRate;
            EmitterLeft.projectileLifetime = sevenLifetime;
            EmitterLeft.projectileSpeed = sevenSpeed;
            EmitterCenter.PowerupEnabled = true;
            EmitterLeft.FiringEnabled = true;

        }
        if(EmitterCenter)
        {
            EmitterCenter.firingRate = sevenFiringRate;
            EmitterCenter.projectileLifetime = sevenLifetime;
            EmitterCenter.projectileSpeed = sevenSpeed;
            EmitterCenter.PowerupEnabled = true;
            EmitterCenter.FiringEnabled = true;
            StartCoroutine(PlayDispenseSound());
        }
        if(EmitterRight)
        {
            EmitterRight.firingRate = sevenFiringRate;
            EmitterRight.projectileLifetime = sevenLifetime;
            EmitterRight.projectileSpeed = sevenSpeed;
            EmitterCenter.PowerupEnabled = true;
            EmitterRight.FiringEnabled = true;
        }
    }

    public IEnumerator PlayDispenseSound()
    {
        source.PlayOneShot(dispenseClip);
        yield return new WaitForSeconds(dispenseWaitInSeconds);
        source.PlayOneShot(dispenseClip);
        yield return new WaitForSeconds(dispenseWaitInSeconds);
        source.PlayOneShot(dispenseClip);
        yield return new WaitForSeconds(dispenseWaitInSeconds);
        source.PlayOneShot(dispenseClip);
        yield return new WaitForSeconds(dispenseWaitInSeconds);
        source.PlayOneShot(dispenseClip);
        yield return new WaitForSeconds(dispenseWaitInSeconds);
        source.PlayOneShot(dispenseClip);
        yield return new WaitForSeconds(dispenseWaitInSeconds);
        source.PlayOneShot(dispenseClip);
        yield return new WaitForSeconds(dispenseWaitInSeconds);
    }

}
