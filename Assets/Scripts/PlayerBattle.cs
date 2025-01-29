using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBattle : MonoBehaviour
{
    public GameManager gameManager;
    public Animator playerAnimator;
    public PlayerSpawner bulletSpawner;

    public bool Allowed = false;

    private bool isHurt = false;
    private bool oneFrame = false;

    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gameManager = (GameManager)list.First();
        }
    }

    void Update()
    {
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
    }

    void OnAttack(InputValue value)
    {
        Allowed = value.isPressed;

        if(Allowed)
        {
            bulletSpawner.FiringEnabled = true;
        }
        else
        {
            bulletSpawner.FiringEnabled = false;
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
        }
    }

    
}
