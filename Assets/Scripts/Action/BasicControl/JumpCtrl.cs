using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class JumpCtrl : BaseMovement
{
    private Vector2 _velocity;

    private float _timeJumpWasPressed, _timeLeftGround = float.MinValue;
    private float _jumpPower;

    private bool _isJumpCutoffApplied;
    private bool _isJumping;
    private bool _isFalling;

    private int _jumpLeft;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        _timeJumpWasPressed -= Time.deltaTime;
        
        if (PlayerCtrl.instance.JumpDown) OnJumpDown();
        if (PlayerCtrl.instance.JumpReleased) OnJumpReleased();
        
    }
    private void FixedUpdate()
    {   
        _velocity = _rb.velocity;
        JumpCheck();
        _anim.SetBool("Jumping", _isJumping);
        _anim.SetBool("Falling", _isFalling);
        _anim.SetFloat("YAxis", _rb.velocity.y);

        ApplyMovement(_velocity);
    }

    void OnJumpDown()
    {
        _timeJumpWasPressed = _stat.JumpBufferTime;
    }
    void OnJumpReleased()
    {
        if (_rb.velocity.y > 0 && !_isJumpCutoffApplied && _isJumping)
            _rb.velocity *= new Vector2(1, _stat.JumpCutOffMultipiler);    
        _isJumpCutoffApplied = true;
    }

    void JumpCheck()
    {
        if (_collisionCtrl.OnGround && _rb.velocity.y == 0)
        {
            _timeLeftGround = _stat.JumpCoyoteTime;
            _isJumpCutoffApplied = false;
            _jumpLeft = _stat.JumpCount;
        }
        else
        {
            _timeLeftGround -= Time.fixedDeltaTime;
        }
        if (_isJumping && _rb.velocity.y < 0)
        {
            _isJumping = false;
            _isFalling = true;
        }
        if (_timeLeftGround > 0 && !_isJumping)
        {
            _isJumpCutoffApplied = false;
            if (!_isJumping)
            {
                _isFalling = false;
            }
        }
        if ( !_isJumping 
            && _timeJumpWasPressed > 0
            && (_timeLeftGround > 0 || _jumpLeft > 0))
        {
            _isJumping = true;
            _isFalling = false;

            if (_timeLeftGround <= 0) _jumpLeft--;
            HandleJumping();
        }
    }

    void HandleJumping()
    {
        _timeJumpWasPressed = 0;
        _timeLeftGround = 0;

        _jumpPower = Mathf.Sqrt(_stat.JumpHeight * (Physics2D.gravity.y * _rb.gravityScale) * -2f) * _rb.mass; //Sqrt(-2gh)
        if (_velocity.y < 0f)
        {
            _jumpPower = Mathf.Max(_jumpPower - _velocity.y, 0f);
        }
        _velocity.y += _jumpPower;

    }

}
