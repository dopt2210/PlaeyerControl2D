using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class JumpCtrl : MonoBehaviour
{
    [SerializeField] UseableStats _stat;
    private PlayerCtrl _playerCtrl;
    private CollisionCtrl _collisionCtrl;
    private Rigidbody2D _rb;
    private Vector2 _velocity;

    private float timeJumpWasPressed, timeLeftGround = float.MinValue;
    private float JumpPower;

    private bool jumpCutoffApplied;
    private bool isCoyoteTime;
    private bool _jumpReq;
    //private bool isJumping;

    private int JumpLeft;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerCtrl = GetComponent<PlayerCtrl>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }
    private void Update()
    {
        if (_playerCtrl._input.JumpDown) _jumpReq = true;
    }
    private void FixedUpdate()
    {
        _collisionCtrl.CheckCollision();
        _velocity = _rb.velocity;

        JumpOrder();
    }
    
    

    public void JumpOrder()
    {
        // Coyote time
        if (_collisionCtrl.isGrounded && _rb.velocity.y == 0)
        {
            JumpLeft = 0;
            timeLeftGround = _stat.CoyoteTime;
            isCoyoteTime = false;

            //isJumping = false;
            jumpCutoffApplied = false;
        }
        else
        {
            timeLeftGround -= Time.deltaTime;
        }

        if (_jumpReq)
        {
            _jumpReq = false;
            HandleJumping();
        }

        if (!_playerCtrl._input.JumpHeld && _rb.velocity.y > 0 && !jumpCutoffApplied)
        {
            _velocity.y *= _stat.JumpCutOff;
            jumpCutoffApplied = true;
        }

        _rb.velocity = _velocity;
    }

    void HandleJumping()
    {
        if (timeLeftGround > 0 || (JumpLeft < _stat.JumpCount && isCoyoteTime))
        {
            timeLeftGround = 0;
            isCoyoteTime = true;

            if (isCoyoteTime)
                JumpLeft++;
            JumpPower = Mathf.Sqrt(_stat.JumpHeight * (Physics2D.gravity.y * _rb.gravityScale) * -2) * _rb.mass;
            if (_velocity.y > 0f)
            {
                JumpPower = Mathf.Max(JumpPower - _velocity.y, 0f);
            }
            _velocity.y += JumpPower;
            //isJumping = true;
        }
        _rb.velocity = _velocity;
    }
}
