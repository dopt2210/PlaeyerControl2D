using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MoveCtrl : MonoBehaviour
{
    [SerializeField] public UseableStats _stat;

    private Rigidbody2D _rb;
    private CollisionCtrl _collisionCtrl;
    [SerializeField] Animator anim;

    [SerializeField] private Vector2 _velocity;

    [SerializeField] private float _acceleration, _speedModifier, _maxSpeed, _speedChange;

    private void Awake()
    {
        _collisionCtrl = GetComponent<CollisionCtrl>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _velocity = _rb.velocity;

        //HandleWalking();
        HanldleMove();
        
        anim.SetFloat("Move", Mathf.Abs(_maxSpeed));
        anim.SetBool("OnGround", _collisionCtrl.OnGround());
    }
    void HandleWalking()
    {
        _maxSpeed = PlayerCtrl.Move.x * _stat.WalkSpeed;
        _acceleration = _collisionCtrl.OnGround() ? _stat.Acceleration : _stat.Deceleration;

        _speedModifier = _acceleration * Time.deltaTime;

        _velocity.x = Mathf.MoveTowards(_rb.velocity.x, _maxSpeed, _speedModifier);
        _rb.velocity = _velocity;

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
