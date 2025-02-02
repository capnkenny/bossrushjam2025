using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBattleFB : MonoBehaviour
{
    public GameManager gameManager;
    public Animator playerAnimator;
    public PlayerSpawnerFB bulletSpawner;
    
    public AudioSource audioSource;
    public AudioClip coinPickup;

    public bool Allowed = false;

    private bool isHurt = false;
    private bool oneFrame = false;

    private InputAction attackAction;

    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }

        attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        if(gameManager)
        {
            bulletSpawner.PowerUpMode = gameManager.PlayerPowerUpMode;
        }

        if(playerAnimator && isHurt)
        {
            if(oneFrame)
            {
            playerAnimator.ResetTrigger("Hurt");
            isHurt = false;
            oneFrame = false;
            }
            else
            {oneFrame = true;}
        }
        
        if(bulletSpawner && gameManager)
        {
            var currency = gameManager.GetPlayerCurrency();
            if(attackAction.IsPressed() && currency > 0)
            {
                bulletSpawner.FiringEnabled = true;
                gameManager.AddToCurrency(-1);
            }

        }            
    }
    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            var obj = collision.gameObject;
            var proj = obj.GetComponent<Projectile>();

            //hurt the player if gm is not null
            if(gameManager && !isHurt)
            {
                gameManager.PlayerHealth.DmgUnit(1);
                if(gameManager.PlayerHealth._currentHealth > 0)
                {
                    playerAnimator.SetTrigger("Hurt");
                    isHurt = true;
                }
            }
            //destroy the projectile
            if(obj && proj)
            {
                proj.Delete();
            }
        }

        //Currency handling during game
        if(collision.gameObject.tag == "Currency")
        {
            var obj = collision.gameObject;
            var proj = obj.GetComponent<Projectile>();
            if(gameManager)
            {
                gameManager.AddToCurrency(1);
            }
            if(obj && proj)
            {
                proj.Delete();
            }
            if(audioSource && coinPickup)
            {
                audioSource.PlayOneShot(coinPickup);
            }
        }
    }

    
}
