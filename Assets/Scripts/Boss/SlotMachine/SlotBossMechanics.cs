using System.Linq;
using UnityEngine;

public class SlotBossMechanics : MonoBehaviour
{
    public GameManager gameManager;
    public UnitHealth BossHealth;
    public Animator bossAnimator;
    public Animator ReelOneAnimator;
    public Animator ReelTwoAnimator;
    public Animator ReelThreeAnimator;
    private bool isHurt = false;
    private bool oneFrame = false;

    private int oneSixthHealth = 150;
    private int oneThirdHealth = 334;
    private int twoThirdsHealth = 667;

    private bool veryHurt = false;
    private bool critHurt = false;
    private bool dead = false;
    private bool veryHurtTriggered = false;
    private bool critHurtTriggered = false;
    private bool almostDeadTriggered = false;
    private bool deadTriggered = false;


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
        }
    }

    void Update()
    {
        CheckIfHurt();

        // if(bossAnimator && isHurt)
        // {
        //     if(oneFrame)
        //     {
        //         bossAnimator.ResetTrigger("Hurt");
        //         isHurt = false;
        //         oneFrame = false;
        //     }
        //     else
        //     {
        //         oneFrame = true;
        //     }
        // }
    }

    private void CheckIfHurt()
    {
        if(!veryHurt && BossHealth._currentHealth <= twoThirdsHealth)
        {
            veryHurt = true;
            bossAnimator.SetTrigger("Hurt");
            ReelOneAnimator.SetTrigger("Lucky7");
        }
        else if (veryHurt && !critHurt)
        {
            if(!veryHurtTriggered)
                veryHurtTriggered = true;

            if(BossHealth._currentHealth <= oneThirdHealth)
            {
                critHurt = true;
                bossAnimator.SetTrigger("Hurt");
                ReelThreeAnimator.SetTrigger("Lucky7");
            }
        }
        else if (veryHurt && critHurt)
        {
            if(!critHurtTriggered)
                critHurtTriggered = true;

            if(BossHealth._currentHealth <= oneSixthHealth && BossHealth._currentHealth > 0)
            {
                ReelTwoAnimator.SetTrigger("Lucky7");
                bossAnimator.SetTrigger("Hurt");
                almostDeadTriggered = true;
            }
            else
            {
                bossAnimator.SetTrigger("Dead");
                dead = true;
            }

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            if(BossHealth)
            {
                var proj = collision.gameObject.GetComponent<Projectile>();
                if(proj)
                    BossHealth.DmgUnit(proj.DamageValue);
                else
                    BossHealth.DmgUnit(1);
            }
        }
    }
}
