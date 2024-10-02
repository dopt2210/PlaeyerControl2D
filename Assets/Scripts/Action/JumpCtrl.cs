using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class JumpCtrl : MonoBehaviour
{
    [SerializeField] private UseableStats _stat;
    private PlayerCtrl _playerCtrl;
    private CollisionCtrl _collisionCtrl;
    private Rigidbody2D _rb;

    private Vector2 _velocity;

    private float _timeJumpWasPressed, _timeLeftGround = float.MinValue;
    private float _jumpPower;

    private bool _isJumpCutoffApplied;
    private bool _isCoyoteTime;
    private bool _jumpReq;
    //private bool isJumping;

    private int _jumpLeft;

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
        _velocity = _rb.velocity;   //de co the khoa velocity cua _rb toan cuc, chi thay doi velocity cuc bo

        JumpOrder();
    }
    public void JumpOrder()
    {
        // Coyote time
        if (_collisionCtrl.OnGround() && _rb.velocity.y == 0)
        {
            _jumpLeft = 0;
            _timeLeftGround = _stat.CoyoteTime; //dat bang thoi gian co the nhay tiep khi roi khoi mat dat
            _isCoyoteTime = false;

            //isJumping = false;
            _isJumpCutoffApplied = false;   //khong con cat thoi gian nhay
        }
        else
        {
            _timeLeftGround -= Time.deltaTime;  //neu roi mat dat thi giam dan coyote time
        }

        if (_jumpReq)
        {
            _jumpReq = false;   //khong con update
            
            _timeJumpWasPressed = _stat.BufferJump; //dat bang thoi gian tu luc thuc hien nhay
        }
        else if(!_jumpReq && _timeJumpWasPressed > 0)
        {
            _timeJumpWasPressed -= Time.deltaTime;
        }

        if (_timeJumpWasPressed > 0)    //neu con thoi gian thuc hien nhay thi co the nhay
        {
            HandleJumping();    //thuc hien nhay
        }

        if (!_playerCtrl._input.JumpHeld && _rb.velocity.y > 0 && !_isJumpCutoffApplied) 
        {
            _velocity.y *= _stat.JumpCutOff;    //giam 1 nua khoang cach nhay
            _isJumpCutoffApplied = true;
        }

        _rb.velocity = _velocity;
    }

    void HandleJumping()
    {
        if (_timeLeftGround > 0 || (_jumpLeft < _stat.JumpCount && _isCoyoteTime))
        {
            _timeLeftGround = 0;
            _isCoyoteTime = true;

            if (_isCoyoteTime) _jumpLeft++; //Nhay nhieu lan neu con coyote
            _jumpPower = Mathf.Sqrt(_stat.JumpHeight * (Physics2D.gravity.y * _rb.gravityScale) * -2) * _rb.mass;
            if (_velocity.y > 0f)
            {
                _jumpPower = Mathf.Max(_jumpPower - _velocity.y, 0f);
            }
            _velocity.y += _jumpPower;
            //isJumping = true;
        }
        _rb.velocity = _velocity;
    }
}
