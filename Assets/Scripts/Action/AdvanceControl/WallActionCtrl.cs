using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WallActionCtrl : BaseMovement
{
    private Vector2 _velocity;

    private Vector2 _wallJumpDirection = new Vector2(1, 1);

    private float _holdCounter;

    private bool _jumpWallReq;
    private bool _isWallJumping;
    bool _isClimb;

    private void OnEnable()
    {
        _rb.gravityScale = _stat.DefaultGravityScale;
        _holdCounter = _stat.WallHoldTime;
    }
    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        if ((_collisionCtrl.OnWallRight || _collisionCtrl.OnWallLeft) && !_collisionCtrl.OnGround && PlayerCtrl.instance.JumpDown) _jumpWallReq = true;
    }
    private void FixedUpdate()
    {
        _velocity = _rb.velocity;

        WallOrder();
        HandleWallJumping();
        _anim.SetBool("Climb", _isClimb);
    }

    public void WallOrder()
    {

        if (_collisionCtrl.OnWallRight || _collisionCtrl.OnWallLeft)
        {
            if (PlayerCtrl.instance.ClimbDown && _holdCounter > 0)
            {
                _isClimb = true;
                if (PlayerCtrl.instance.Move.y > 0)
                {
                    _velocity.y = _stat.WallClimbSpeed;
                    _rb.gravityScale = 0;
                    _holdCounter -= Time.fixedDeltaTime;
                }
                if (PlayerCtrl.instance.Move.y < 0)
                {
                    _velocity.y = -_stat.WallClimbSpeed;
                    _rb.gravityScale = 0;
                    _holdCounter -= Time.fixedDeltaTime;
                }
                if (PlayerCtrl.instance.Move.y == 0)
                {
                    _velocity.y = 0;
                    _rb.gravityScale = 0;
                    _holdCounter -= Time.fixedDeltaTime;
                }
            }
            else if (_collisionCtrl.OnGround)
            {
                _holdCounter = _stat.WallHoldTime;
                _isClimb=false;
            }
            else
            {
                _isClimb = false;
                _rb.drag = _stat.WallSlideSpeed;
                _rb.gravityScale = _stat.DefaultGravityScale;
            }

        }
        else
        {
            _isClimb = false;
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
            if (_collisionCtrl.OnWallLeft)
            {
                jumpDirection = new Vector2(_wallJumpDirection.x, _wallJumpDirection.y);
            }
            else if (_collisionCtrl.OnWallRight)
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
