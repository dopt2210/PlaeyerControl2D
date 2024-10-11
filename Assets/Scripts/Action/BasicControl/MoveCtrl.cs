using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MoveCtrl : BaseMovement
{
    [SerializeField] private Vector2 _velocity;

    [SerializeField] private float _acceleration, _speedModifier, _maxSpeed, _speedChange;

    private bool _isFacingRight;

    protected override void Awake()
    {
        base.Awake();
        _isFacingRight = true;
    }

    private void FixedUpdate()
    {
        _velocity = _rb.velocity;
        SetFacingDirection(PlayerCtrl.instance.Move);
        HanldleMove();
        
        _anim.SetFloat("Move", Mathf.Abs(_maxSpeed));
        _anim.SetBool("OnGround", _collisionCtrl.OnGround);

        ApplyMovement(_velocity);
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
    void HanldleMove()
    {

        _maxSpeed = PlayerCtrl.instance.Move.x * _stat.WalkSpeed;
        
        _maxSpeed = Mathf.Lerp(_rb.velocity.x, _maxSpeed, 1f);

        _speedChange = _maxSpeed - _rb.velocity.x;

        _acceleration = (Mathf.Abs(_maxSpeed) > 0.01) && _collisionCtrl.OnGround ? _stat.Acceleration : _stat.Deceleration;

        _speedModifier = Mathf.Pow(Mathf.Abs(_acceleration * _speedChange), _stat.AccelerationPower) * Time.fixedDeltaTime;

        _velocity.x = Mathf.MoveTowards(_rb.velocity.x, _maxSpeed, _speedModifier/_rb.mass);
        if(_velocity.y > -1e-5f && _velocity.y < 1e-5f) { _velocity.y = 0; }
    }

}
