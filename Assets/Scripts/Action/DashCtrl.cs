using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class DashCtl : MonoBehaviour
{
    [SerializeField] UseableStats _stat;
    private PlayerCtrl _playerCtrl;
    private CollisionCtrl _collisionCtrl;
    private Rigidbody2D _rb;
    private Vector2 _velocity;

    private Vector2 dashDirection;

    private bool isDash = true;
    private bool isDashing;
    private bool _dashReq;

    private float dashCounter;

    private void Awake()
    {
        _playerCtrl = GetComponent<PlayerCtrl>();
        _rb = GetComponent<Rigidbody2D>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }
    private void Update()
    {
        if (_playerCtrl._input.DashDown && isDash) _dashReq = true;
        DashOrder();
    }
    private void FixedUpdate()
    {
        _collisionCtrl.CheckCollision();
        _velocity = _rb.velocity;
    }

    public void DashOrder()
    {
        if (_dashReq)
        {
            _dashReq = false;
            isDashing = true;
            isDash = false;

            dashCounter = _stat.DashCooldown;

            dashDirection = new Vector2(_playerCtrl._input.Move.x, _playerCtrl._input.Move.y).normalized;
            if (dashDirection == Vector2.zero)
            {
                dashDirection = new Vector2(transform.localScale.x, 0f);
            }

            StartCoroutine(HandleDash());
        }

        if (isDashing)
        {
            //_rb.velocity = dashDirection * _stat.dashSpeed;
            _rb.MovePosition(_rb.position + dashDirection * _stat.DashSpeed);
            return;
        }
        if (_collisionCtrl.isGrounded && dashCounter <= 0.01f) isDash = true;

    }

    private IEnumerator HandleDash()
    {
        yield return new WaitForSeconds(_stat.DashDuration);
        isDashing = false;
        while (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            yield return null;
        }

    }
}
