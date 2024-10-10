using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MoveCtrl : BaseMovement
{
    private Vector2 _velocity;

    private float _acceleration, _speedModifier, _maxSpeed, _speedChange;

    private bool isFacingRight;

    protected override void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        _velocity = _rb.velocity;
        SetFacingDirection(PlayerCtrl.Move);
        
        HanldleMove();
        
        _anim.SetFloat("Move", Mathf.Abs(_maxSpeed));
        _anim.SetBool("OnGround", _collisionCtrl.OnGround());
    }
    
    public void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
            //_spriteRenderer.flipX = false;
            transform.parent.localScale = new Vector2(1, 1);

        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
            //_spriteRenderer.flipX = true;
            transform.parent.localScale = new Vector2(-1, 1);

        }
    }
    void HanldleMove()
    {
        _maxSpeed = PlayerCtrl.Move.x * _stat.WalkSpeed;
        
        _maxSpeed = Mathf.Lerp(_rb.velocity.x, _maxSpeed, 1f);

        _speedChange = _maxSpeed - _rb.velocity.x;

        _acceleration = (Mathf.Abs(_maxSpeed) > 0.01) && _collisionCtrl.OnGround() ? _stat.Acceleration : _stat.Deceleration;

        _speedModifier = Mathf.Pow(Mathf.Abs(_acceleration * _speedChange), _stat.AccelerationPower) * Time.fixedDeltaTime;

        _velocity.x = Mathf.MoveTowards(_rb.velocity.x, _maxSpeed, _speedModifier/_rb.mass);
        _rb.velocity = _velocity;
    }
}
