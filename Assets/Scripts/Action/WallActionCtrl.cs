using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WallActionCtrl : MonoBehaviour
{
    [SerializeField] UseableStats _stat;
    private CollisionCtrl _collisionCtrl;
    private Rigidbody2D _rb;

    private Vector2 _velocity;
    private Vector2 _wallJumpDirection = new Vector2(1, 1);

    [SerializeField] private float _holdCounter;

    private bool _jumpWallReq;
    private bool _isWallJumping;

    private void OnEnable()
    {
        _rb.gravityScale = _stat.DefaultGravityScale;
        _holdCounter = _stat.WallHoldTime;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }
    private void Update()
    {
        if ((_collisionCtrl.OnWallRight() || _collisionCtrl.OnWallLeft()) && !_collisionCtrl.OnGround() && PlayerCtrl.JumpDown) _jumpWallReq = true;
    }
    private void FixedUpdate()
    {
        _velocity = _rb.velocity;

        WallOrder();
        HandleWallJumping();
    }

    public void WallOrder()
    {

        if (_collisionCtrl.OnWallRight() || _collisionCtrl.OnWallLeft())
        {
            if (PlayerCtrl.ClimbDown && _holdCounter > 0)
            {

                if (PlayerCtrl.Move.y > 0)
                {
                    _velocity.y = _stat.WallClimbSpeed;
                    _rb.gravityScale = 0;
                    _holdCounter -= Time.fixedDeltaTime;
                }
                if (PlayerCtrl.Move.y < 0)
                {
                    _velocity.y = -_stat.WallClimbSpeed;
                    _rb.gravityScale = 0;
                    _holdCounter -= Time.fixedDeltaTime;
                }
                if (PlayerCtrl.Move.y == 0)
                {
                    _velocity.y = 0;
                    _rb.gravityScale = 0;
                    _holdCounter -= Time.fixedDeltaTime;
                }
            }
            else if (_collisionCtrl.OnGround())
            {
                _holdCounter = _stat.WallHoldTime;
            }
            else
            {
                _rb.drag = _stat.WallSlideSpeed;
                _rb.gravityScale = _stat.DefaultGravityScale;
            }

        }
        else
        {
            _rb.drag = 0;
            _rb.gravityScale = _stat.JumpFallGravity;
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

            jumpDirection *= _stat.WallJumpForce;

            if (Mathf.Sign(_velocity.x) != Mathf.Sign(jumpDirection.x))
            {
                jumpDirection.x -= _velocity.x;
            }
            if (_velocity.y < 0)
            {
                jumpDirection.y -= _velocity.y;
            }

            _rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        }
    }
}
