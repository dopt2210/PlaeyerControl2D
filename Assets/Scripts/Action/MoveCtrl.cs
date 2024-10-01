using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MoveCtrl : MonoBehaviour
{
    [SerializeField] public UseableStats _stat;

    private Rigidbody2D _rb;
    private CollisionCtrl _collisionCtrl;
    private Vector2 _velocity;
    private PlayerCtrl _playerCtrl;

    private float Acceleration, MaxSpeed;


    private void Awake()
    {
        _playerCtrl = GetComponent<PlayerCtrl>();
        _rb = GetComponent<Rigidbody2D>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        _collisionCtrl.CheckCollision();
        _velocity = _rb.velocity;

        HandleWalking();
    }
    void HandleWalking()
    {
        Acceleration = _collisionCtrl.isGrounded ? _stat.WalkGroundAcceleration : _stat.WalkAirAcceleration;
        MaxSpeed = Acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_rb.velocity.x, _playerCtrl._input.Move.x * _stat.WalkSpeed, MaxSpeed);
        _rb.velocity = _velocity;

    }
}
