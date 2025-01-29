using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileLifetime = 1f;
    public float speed = 1f;
    public int DamageValue = 1;

    private Vector3 _spawnPoint;
    private float _timer = 0f;

    void Start()
    {
        _spawnPoint = transform.position;
    }

    void Update()
    {
        if(_timer > projectileLifetime)
            Destroy(gameObject);
            //gameObject.SetActive(false); <- can convert to reusable bullet array later
        //transform.position = MoveProjectile(_timer);
        _timer += Time.deltaTime;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    private Vector2 MoveProjectile(float time)
    {
        float x = time * speed * -transform.up.x;
        float y = time * speed * -transform.up.y;
        return new Vector2(_spawnPoint.x + x, _spawnPoint.y + y);
    }

}
