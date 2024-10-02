using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WallActionCtrl : MonoBehaviour
{
    [SerializeField] UseableStats _stat;
    private PlayerCtrl _playerCtrl;
    private CollisionCtrl _collisionCtrl;
    private Rigidbody2D _rb;

    private Vector2 _velocity;
    private Vector2 _wallJumpDirection = new Vector2(1, 1);

    private float _defaultGravityScale = 10;
    [SerializeField] private float _holdCounter;

    private bool _jumpWallReq;

    private void OnEnable()
    {
        _rb.gravityScale = _defaultGravityScale;
        _holdCounter = _stat.WallHoldTime;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerCtrl = GetComponent<PlayerCtrl>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }
    private void Update()
    {
        if ((_collisionCtrl.OnWallRight() || _collisionCtrl.OnWallLeft()) && !_collisionCtrl.OnGround() && _playerCtrl._input.JumpDown) _jumpWallReq = true;
    }
    private void FixedUpdate()
    {
        _collisionCtrl.CheckCollision();
        _velocity = _rb.velocity;

        WallOrder();
        HandleWallJumping();
    }

    public void WallOrder()
    {
        if (_collisionCtrl.OnWallRight() || _collisionCtrl.OnWallLeft())
        {
            if (_playerCtrl._input.ClimbDown && _holdCounter > 0)
            {
                if (_playerCtrl._input.Move.y > 0)
                {
                    _velocity.y = _stat.WallSlideSpeed;
                    _rb.gravityScale = 0;
                }
                if (_playerCtrl._input.Move.y < 0)
                {
                    _velocity.y = -_stat.WallSlideSpeed;
                    _rb.gravityScale = 0;
                }
                if (_playerCtrl._input.Move.y == 0)
                {
                    _velocity.y = 0;
                    _rb.gravityScale = 0;
                }

                _holdCounter -= Time.fixedDeltaTime;
            }
            else
            {
                _rb.drag = 0;
                _rb.gravityScale = _defaultGravityScale;
            }

        }
        else
        {
            _rb.drag = 0;
            _rb.gravityScale = _defaultGravityScale;
            _holdCounter = _stat.WallHoldTime;
        }

        _rb.velocity = _velocity;
    }


    
    public void HandleWallJumping()
    {
        if (_jumpWallReq)
        {
            _jumpWallReq = false;
            Vector2 jumpDirection;
            if (_collisionCtrl.OnWallLeft())
            {
                jumpDirection = new Vector2(_wallJumpDirection.x, _wallJumpDirection.y);
            }
            else if (_collisionCtrl.OnWallRight())
            {
                jumpDirection = new Vector2(-_wallJumpDirection.x, _wallJumpDirection.y);
            }
            else
            {
                return;
            }

            _velocity = jumpDirection * _stat.WallJumpForce;
        }

        _rb.velocity = _velocity;
    }
}
