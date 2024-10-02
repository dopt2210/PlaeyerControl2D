using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MoveCtrl : MonoBehaviour
{
    [SerializeField] public UseableStats _stat;

    private Rigidbody2D _rb;
    private CollisionCtrl _collisionCtrl;
    private PlayerCtrl _playerCtrl;

    private Vector2 _velocity;
    
    private float _acceleration, _speedModifier, _maxSpeed;

    private void Awake()
    {
        _playerCtrl = GetComponent<PlayerCtrl>();
        _collisionCtrl = GetComponent<CollisionCtrl>();

        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _collisionCtrl.CheckCollision();
        _velocity = _rb.velocity;

        //HandleWalking();
        HanldleMove();
    }
    void HandleWalking()
    {
        _maxSpeed = _playerCtrl._input.Move.x * _stat.WalkSpeed;
        _acceleration = _collisionCtrl.OnGround() ? _stat.GroundAcceleration : _stat.AirAcceleration;

        _speedModifier = _acceleration * Time.deltaTime;

        _velocity.x = Mathf.MoveTowards(_rb.velocity.x, _maxSpeed, _speedModifier);
        _rb.velocity = _velocity;

    }
    void HanldleMove()
    {
        _maxSpeed = _playerCtrl._input.Move.x * _stat.WalkSpeed;
        _acceleration = (Mathf.Abs(_maxSpeed) > 0.01) && _collisionCtrl.OnGround() ? _stat.GroundAcceleration : _stat.AirAcceleration;
        //_maxSpeedChange = _maxSpeed - _velocity.x;

        _speedModifier = Mathf.Pow(Mathf.Abs(_acceleration) * _acceleration, 0.9f) * Time.fixedDeltaTime;

        _velocity.x = Mathf.MoveTowards(_rb.velocity.x, _maxSpeed, _speedModifier);
        _rb.velocity = _velocity;
    }
}
