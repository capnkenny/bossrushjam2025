using System.Collections;
using UnityEngine;

public class SlotBossMechanics : MonoBehaviour
{
    [Header("Boss Values")]
    [SerializeField] private int health;
    [SerializeField] private int speed;

    [Header("Boss Objects")]
    [SerializeField] private Animator _animator;
    //[SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Boss_Jump leftJump;

    [Header("Slot Mechanics")]
    [SerializeField] private SlotMechanics _mechanics;

    [Header("Landing Zones")]
    [SerializeField] private Transform _landingZone1;
    [SerializeField] private Transform _landingZone2;
    [SerializeField] private Transform _landingZone3;

    [Header("READ ONLY")]
    [SerializeField] private float _jumpTimer;
    [SerializeField] private float _jumpDistance;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpDurationLeft;
    [SerializeField] private float _jumpWait;
    [SerializeField] private int _bossState;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private Vector3 startPos;

    
    public void SetFallingTrigger() {_animator.SetTrigger("Falling");}
    public void SetJumpingTrigger() {_animator.SetTrigger("Jumping");}
    
    void Awake()
    {
        _jumpTimer = 0;
        _bossState = 1;
        _jumpWait = Random.Range(5.0f, 7.0f);
        _jumpDurationLeft = 0;
        startPos = new Vector3();

        if(health == 0)
            health = 9;
        if(speed == 0)
            speed = 2;


        if(leftJump != null)
        {
            leftJump.startFunction = SetFallingTrigger;
            leftJump.endFunction = SetJumpingTrigger;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
