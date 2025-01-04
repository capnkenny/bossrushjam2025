using UnityEngine;
using UnityEngine.InputSystem;

public class TestChar : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _speed;
    [SerializeField] private int _animationSmoothing;

    private Vector2 _movement = new Vector2 { };
    private Vector2 _previousMovement = new Vector2 { };
    private bool _sprinting = false;
    [SerializeField] private float _animationX = 0;
    [SerializeField] private float _animationY = 0;
    private float _idleAnimValue = 0.1f;
    private float _trueSpeed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_animationSmoothing <= 0)
            _animationSmoothing = 2;
    }

    // Update is called once per frame
    void Update()
    {
        float halfSpeed = _speed / 2;

        _trueSpeed += _sprinting ? _speed * Time.deltaTime : halfSpeed * Time.deltaTime;
        if (_sprinting && _trueSpeed >= _speed)
        {
            _trueSpeed = _speed;
        }
        else if (!_sprinting && _trueSpeed >= halfSpeed)
            _trueSpeed = halfSpeed;

        _rigidbody.linearVelocity = new Vector2(_movement.x * _trueSpeed, _movement.y * _trueSpeed);
        SetAnimatorMovement(_movement);
    }

    //Toggles sprinting
    //These are callbacks from Input Events
    void OnSprint(InputValue _)
    {
        _sprinting = !_sprinting;
    }

    void OnMove(InputValue value)
    {
        _previousMovement = _movement;
        _movement = value.Get<Vector2>();
    }

    private void SetAnimatorMovement(Vector2 movementValue)
    {
        float deltaSmoothing = Time.deltaTime * _animationSmoothing;
        if (_movement.x == 0 && _movement.y == 0)
        {
            if (_previousMovement.x > 0)
            {
                _animationX = _idleAnimValue;
            }
            else if (_previousMovement.x < 0)
            {
                _animationX = -_idleAnimValue;
            }
            else
                _animationX = 0;

            if (_previousMovement.y > 0)
            {
                _animationY = _idleAnimValue;
            }
            else if (_previousMovement.y < 0)
            {
                _animationY = -_idleAnimValue;
            }
            else
                _animationY = 0;
        }
        else
        {
            float multiplier = _sprinting ? 1 : 0.3f;

            float horiz = _movement.x * multiplier;
            float vert = _movement.y * multiplier;

            if (Mathf.Abs(_animationY - vert) < 0.1f)
                _animationY = vert;
            if (Mathf.Abs(_animationX - horiz) < 0.1f)
                _animationX = horiz;

            if (_animationX < horiz)
                _animationX += deltaSmoothing;
            else if (_animationX > horiz)
                _animationX -= deltaSmoothing;

            if (_animationY < vert)
                _animationY += deltaSmoothing;
            else if (_animationY > vert)
                _animationY -= deltaSmoothing;
        }

        _animator.SetFloat("x", _animationX);
        _animator.SetFloat("y", _animationY);
    }
}
