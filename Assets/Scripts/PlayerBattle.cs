using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBattle : MonoBehaviour
{
    public GameManager gameManager;
    public Animator playerAnimator;
    public PlayerSpawner bulletSpawner;
    
    public AudioSource audioSource;
    public AudioClip coinPickup;
    public AudioClip hurtPickup;

    public bool Allowed = false;

    private bool isHurt = false;
    private bool oneFrame = false;

    private InputAction attackAction;
    private PlayerMovement player;

    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }

        attackAction = InputSystem.actions.FindAction("Attack");
        player = FindObjectOfType<PlayerMovement>();

    }

    void Update()
    {
        if(gameManager && bulletSpawner!= null)
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
        
        if(bulletSpawner && player.BattleMode == true)
            bulletSpawner.FiringEnabled = attackAction.IsPressed();
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
                    if(!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(hurtPickup);
                    }
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
