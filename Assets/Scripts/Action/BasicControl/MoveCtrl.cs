using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MoveCtrl : BaseMovement
{

    [SerializeField] private float _acceleration, _speedModifier, _maxSpeed, _speedChange;
    public static bool _isFacingRight {  get; private set; }

    protected override void Awake()
    {
        loadComponent();
        _isFacingRight = true;
    }

    private void FixedUpdate()
    {
        SetFacingDirection(PlayerCtrl.instance.Move);
        HanldleMove(1);
        
        _anim.SetFloat("Move", Mathf.Abs(_maxSpeed));
        _anim.SetBool("OnGround", _collisionCtrl.OnGround);
    }
    
    public void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
            transform.parent.localScale = new Vector2(1, 1);
        }
        else if (moveInput.x < 0 && _isFacingRight)
        {
            _isFacingRight = false;
            transform.parent.localScale = new Vector2(-1, 1);
        }
    }
    void HanldleMove(float learpAmount)
    {
        
        _maxSpeed = PlayerCtrl.instance.Move.x * _stat.WalkSpeed;
        
        _maxSpeed = Mathf.Lerp(_rb.velocity.x, _maxSpeed, learpAmount);

        _speedChange = _maxSpeed - _rb.velocity.x;

        _acceleration = (Mathf.Abs(_maxSpeed) > 0.01) && _collisionCtrl.OnGround ? _stat.Acceleration : _stat.Deceleration;

        _speedModifier = _speedChange * _acceleration;

        //_speedModifier = Mathf.Pow(Mathf.Abs(_acceleration * _speedChange), _stat.AccelerationPower) * Time.fixedDeltaTime;

        //_velocity.x = Mathf.MoveTowards(_rb.velocity.x, _maxSpeed, _speedModifier / _rb.mass);
        //if(Mathf.Abs(_rb.velocity.x) < 1e-5f || Mathf.Abs(_rb.velocity.y) < 1e-5f) {_rb.velocity = Vector2.zero; }
        _rb.AddForce( _speedModifier * Vector2.right, ForceMode2D.Force);
        //if (Mathf.Abs(_rb.velocity.x) < 1e-5f || Mathf.Abs(_rb.velocity.y) < 1e-5f) _rb.velocity = Vector2.zero;
    }

}
