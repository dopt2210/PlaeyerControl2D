using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class DashCtl : BaseMovement
{
    private Vector2 _dashDirection;

    [HideInInspector] private TrailRenderer _tr;
    public static bool _isDashing { get; private set; }

    private bool _isCanDash = true;
    
    private bool _dashReq;//for update

    private float _dashCounter;

    protected override void Awake()
    {
        loadComponent();
        _tr = transform.parent.GetComponentInChildren<TrailRenderer>();
    }
    private void Update()
    {
        DashOrder();
    }

    public void DashOrder()
    {
        if (PlayerCtrl.instance.DashDown && _isCanDash) _dashReq = true;

        if (_dashReq)
        {
            _dashReq = false;
            _isDashing = true;
            _isCanDash = false;
            _anim.SetBool("Dash", _isDashing);
            _tr.emitting = true;
            _dashCounter = _stat.DashCooldown;

            _dashDirection = new Vector2(PlayerCtrl.instance.Move.x, PlayerCtrl.instance.Move.y).normalized;
            if (_dashDirection == Vector2.zero)
            {
                _dashDirection = new Vector2(transform.parent.localScale.x, 0f);
            }

            StartCoroutine(HandleDash());
        }

        if (_isDashing)
        {
            //_rb.velocity = _dashDirection * _stat.DashSpeed;
            _rb.MovePosition(_rb.position + _dashDirection * _stat.DashSpeed);
            return;
        }
        if (_collisionCtrl.OnGround && _dashCounter <= 0.01f) _isCanDash = true;

    }
    private IEnumerator HandleDash()
    {
        yield return new WaitForSeconds(_stat.DashDuration);
        _isDashing = false;
        _anim.SetBool("Dash", _isDashing);
        _tr.emitting = false;
        while (_dashCounter > 0)
        {
            _dashCounter -= Time.deltaTime;
            yield return null;
        }

    }
}
