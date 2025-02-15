using System.Collections;
using TMPro;
using UnityEngine;

public class DashCtl : BaseMovement
{
    public static bool _isDashing { get; private set; }
    
    private Vector2 _dashDirection;
    public static bool _isCanDash { get; set; }
    [SerializeField] private bool _dashReq;//for update

    [SerializeField] private static float _dashCounter;

    private void Awake()
    {
        LoadComponents();
    }
    private void Update()
    {
        DashOrder();
    }
    #region Dash Input
    public void DashOrder()
    {
        _tr.emitting = false;
        if (PlayerCtrl.instance.DashDown && _isCanDash) _dashReq = true;

        if (_dashReq)
        {
            _dashReq = false;
            _isDashing = true;
            _isCanDash = false;
            _anim.SetBool("Dash", _isDashing);
            
            _dashCounter = _stat.DashCooldown;

            _dashDirection = new Vector2(PlayerCtrl.instance.MoveX, PlayerCtrl.instance.MoveY).normalized;
            if (_dashDirection == Vector2.zero)
            {
                _dashDirection = new Vector2(transform.parent.localScale.x, 0f);
            }

            StartCoroutine(HandleDash());

            
        }
        if (_isDashing)
        {
            //_rb.velocity = _dashDirection * _stat.DashSpeed;
            _tr.emitting = true;
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
    public static void resetDash()
    {
        _isDashing = false;
        _dashCounter = 0;
    }
    #endregion
}
