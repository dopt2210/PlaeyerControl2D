using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class DashCtl : MonoBehaviour
{
    [SerializeField] private UseableStats _stat;
    private CollisionCtrl _collisionCtrl;
    private Rigidbody2D _rb;
    [SerializeField] private Animator _anim;

    private Vector2 _velocity;
    private Vector2 _dashDirection;

    private bool _isDash = true;
    private bool _isDashing;
    private bool _dashReq;

    private float _dashCounter;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }
    private void Update()
    {
        DashOrder();
        
    }
    private void FixedUpdate()
    {
        _velocity = _rb.velocity;
    }
    public void DashOrder()
    {
        if (PlayerCtrl.DashDown && _isDash) _dashReq = true;

        if (_dashReq)
        {
            _anim.SetBool("Dash", true);
            _dashReq = false;
            _isDashing = true;
            _isDash = false;

            _dashCounter = _stat.DashCooldown;

            _dashDirection = new Vector2(PlayerCtrl.Move.x, PlayerCtrl.Move.y).normalized;
            if (_dashDirection == Vector2.zero)
            {
                _dashDirection = new Vector2(transform.localScale.x, 0f);
            }

            StartCoroutine(HandleDash());
        }

        if (_isDashing)
        {
            //_rb.velocity = _dashDirection * _stat.dashSpeed;
            _rb.MovePosition(_rb.position + _dashDirection * _stat.DashSpeed);
            return;
        }
        if (_collisionCtrl.OnGround() && _dashCounter <= 0.01f) _isDash = true;

    }
    private IEnumerator HandleDash()
    {
        yield return new WaitForSeconds(_stat.DashDuration);
        _isDashing = false;
        _anim.SetBool("Dash", false);
        while (_dashCounter > 0)
        {
            _dashCounter -= Time.deltaTime;
            yield return null;
        }

    }
}
