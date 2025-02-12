using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float projectileLifetime = 1f;
    public float speed = 1f;
    public int DamageValue = 1;

    private Vector3 _spawnPoint;
    private float _timer = 0f;

    private GameManager gm;
    private Vector2 _cachedForce = Vector2.zero;

    void Start()
    {
        _spawnPoint = transform.position;

         var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gm = list.First();
        }
    }

    void Update()
    {
        if(!gm.Paused)
        {
            if(rb2d.linearVelocity == Vector2.zero && _cachedForce != Vector2.zero)
            {
                rb2d.linearVelocity = _cachedForce;
                _cachedForce = Vector2.zero;
                rb2d.gravityScale = 1;
            }

            if(_timer > projectileLifetime)
                Destroy(gameObject);

            _timer += Time.deltaTime;
        }
        else
        {
            if(_cachedForce == Vector2.zero)
            {
                _cachedForce = rb2d.linearVelocity;
                rb2d.linearVelocity = Vector2.zero;
                rb2d.gravityScale = 0;
            }
        }

    }

    public void Delete()
    {
        Destroy(gameObject);
    }

}
