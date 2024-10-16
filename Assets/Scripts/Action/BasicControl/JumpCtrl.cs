using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class JumpCtrl : BaseMovement
{
    public static bool _isJumping { get; private set; }
    public static bool _isFalling { get; private set; }

    [SerializeField] private float _timeJumpWasPressed, _timeLeftGround = float.MinValue;
    [SerializeField] private float _jumpPower;

    private bool _isJumpCutoffApplied;
    private int _jumpLeft;

    protected override void Awake()
    {
        loadComponent();

    }
    private void Update()
    {
        _timeJumpWasPressed -= Time.deltaTime;
        
        if (PlayerCtrl.instance.JumpDown) OnJumpDown();
        if (PlayerCtrl.instance.JumpReleased) OnJumpReleased();
    }
    private void FixedUpdate()
    {   
        JumpCheck();
        _anim.SetBool("Jumping", _isJumping);
        _anim.SetBool("Falling", _isFalling);
        _anim.SetFloat("YAxis", _rb.velocity.y);
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
    private bool IsCanJump => _collisionCtrl.OnGround && !_isJumping;

    void JumpCheck()
    {
        if (_collisionCtrl.OnGround)
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
            if(!WallActionCtrl._isWallJumping)
                _isFalling = true;
        }

        if (_timeLeftGround > 0 && !_isJumping && !WallActionCtrl._isWallJumping)
        {
            _isJumpCutoffApplied = false;
            if (!_isJumping)
            {
                _isFalling = false;
            }
        }
        if ( IsCanJump
            && _timeJumpWasPressed > 0
            && (_timeLeftGround > 0 || _jumpLeft > 0))
        {
            _isJumping = true;
            _isFalling = false;

            if (_timeLeftGround <= 0) _jumpLeft--;
            HandleJumping();
        }
        else if (WallActionCtrl._isWallJumping)
        {
            _isJumping = false;
            _isFalling = false;
        }
    }

    void HandleJumping()
    {
        _timeJumpWasPressed = 0;
        _timeLeftGround = 0;

        //_jumpPower = Mathf.Sqrt(_stat.JumpHeight * (Physics2D.gravity.y * _rb.gravityScale) * -2f) * _rb.mass; //Sqrt(-2gh)
        _jumpPower = _stat.JumpForce;
        if (_rb.velocity.y < 0f)
        {
            //_jumpPower = Mathf.Max(_jumpPower - _rb.velocity.y, 0f);
            _jumpPower -= _rb.velocity.y;
        }
        _rb.AddForce(_jumpPower * Vector2.up, ForceMode2D.Impulse);

    }

}
