using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SlotBossMechanics : MonoBehaviour
{
    public GameManager gameManager;
    public UnitHealth BossHealth;
    public Animator bossAnimator;
    public Animator ReelOneAnimator;
    public Animator ReelTwoAnimator;
    public Animator ReelThreeAnimator;
    public Material bossMaterial;
    public SpriteRenderer bossSprite;
    public AudioClip jumpClip;
    public AudioSource source;


    public int oneSixthHealth = 150;
    public int oneThirdHealth = 334;
    public int twoThirdsHealth = 667;

    private bool veryHurt = false;
    private bool critHurt = false;
    public bool dead = false;
    private bool veryHurtTriggered = false;
    private bool critHurtTriggered = false;
    private bool almostDeadTriggered = false;
    private bool deadTriggered = false;
    private Color origColor;


    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }

        if(BossHealth)
        {
            oneThirdHealth = BossHealth._currentHealth / 3;
            twoThirdsHealth = oneThirdHealth * 2;
            oneSixthHealth = oneThirdHealth / 2;
            Debug.Log("Boss values: "+ oneThirdHealth + " " + twoThirdsHealth + " " + oneSixthHealth + " ");
        }
        origColor = bossSprite.color;
    }

    void Update()
    {
        if(bossMaterial)
        {
            bossMaterial.SetFloat("_Noise", Random.Range(67.0f, 200f));
        }
        CheckIfHurt();

        
    }

    private void CheckIfHurt()
    {
        if(!veryHurt && BossHealth._currentHealth <= twoThirdsHealth)
        {
            veryHurt = true;
            StartCoroutine(HurtAnim());
            ReelOneAnimator.SetTrigger("Lucky7");
        }
        else if (veryHurt && !critHurt)
        {
            if(BossHealth._currentHealth <= oneThirdHealth)
            {
                critHurt = true;
                StartCoroutine(HurtAnim());
                ReelThreeAnimator.SetTrigger("Lucky7");
            }
            if(!veryHurtTriggered)
                veryHurtTriggered = true;

        }
        else if (veryHurt && critHurt)
        {
            if(BossHealth._currentHealth <= oneSixthHealth && BossHealth._currentHealth > 0)
            {
                ReelTwoAnimator.SetTrigger("Lucky7");
                StartCoroutine(HurtAnim());
                almostDeadTriggered = true;
            }
            else if(!dead)
            {
                dead = true;
                StartCoroutine(DeathAnim());
            }

            if(!critHurtTriggered)
                critHurtTriggered = true;
        }
        else if(dead)
        {
            if(deadTriggered)
            {
                Vector3 vec = transform.position;
                vec.y -= 20 * Time.deltaTime;
                transform.position = vec;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            var proj = collision.gameObject.GetComponent<Projectile>();
            if(BossHealth)
            {
                if(proj)
                    BossHealth.DmgUnit(proj.DamageValue);
                else
                    BossHealth.DmgUnit(1);
            }

            if(proj)
                proj.Delete();
        }
    }

    public IEnumerator HurtAnim()
    {
        Debug.Log("Hurting boss...");
        if(critHurt && !critHurtTriggered)
        {
            bossMaterial.SetInt("ApplyEffect", 1);
        }

        Color c = bossSprite.color;
        bossSprite.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = c;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = c;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = c;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        bossSprite.color = Color.black;
        yield return new WaitForSeconds(0.25f);
        if(!almostDeadTriggered)
            bossSprite.color = c;
        else
            bossSprite.color = new Color(c.r + Random.Range(c.r-100, c.r+100), 
                0,
                0,
                c.a);
    }

    public void PlayJumpSound()
    {
        if(jumpClip && source)
        {
            source.PlayOneShot(jumpClip);
        }
    }

    public IEnumerator DeathAnim()
    {
        bossSprite.color = origColor;
        bossAnimator.SetBool("Dead", true);
        bossAnimator.Play("Idle");
        bossAnimator.StopPlayback();
        yield return new WaitForSeconds(1.0f);
        deadTriggered = true;
    }

}
