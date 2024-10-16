using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WallActionCtrl : BaseMovement
{
    public static bool _isWallClimbing { get; private set; }
    public static bool _isWallJumping { get; private set; }
    public static bool _isWallSliding { get; private set; }

    [SerializeField] private Vector2 _velocity;
    private Vector2 _wallJumpDirection = new Vector2(1, 1);
    [SerializeField] private float _holdCounter;

    private bool _jumpWallReq;//for update

    private void Start()
    {
        _holdCounter = _stat.WallHoldTime;
    }
    protected override void Awake()
    {
        loadComponent();
    }
    private void Update()
    {
        checkWallJump();
    }
    private void FixedUpdate()
    {
        _velocity = _rb.velocity;
        WallOrder();
        _anim.SetBool("Climb", _isWallClimbing);

    }
    public bool IsCanWallSlide =>!_isWallJumping && !_collisionCtrl.OnGround && (_collisionCtrl.OnWallRight || _collisionCtrl.OnWallLeft);
    #region Slide
    public void WallOrder()
    {
        if (IsCanWallSlide)
        {
            _isWallSliding = true;
            Debug.Log("IsSlide");
            if (PlayerCtrl.instance.ClimbDown && _holdCounter > 0)
            {
                _isWallClimbing = true;
                Debug.Log("IsClimb");
                if (PlayerCtrl.instance.Move.y > 0)
                {
                    _velocity.y = _stat.WallClimbSpeed;
                    _holdCounter -= Time.fixedDeltaTime;
                }
                if (PlayerCtrl.instance.Move.y < 0)
                {
                    _velocity.y = -_stat.WallClimbSpeed;
                    _holdCounter -= Time.fixedDeltaTime;
                }
                if (PlayerCtrl.instance.Move.y == 0)
                {
                    _velocity.y = 0;
                    _holdCounter -= Time.fixedDeltaTime;
                }
            }
            if(_isWallSliding) 
            {
                if(_collisionCtrl.OnGround) _holdCounter = _stat.WallHoldTime;
                _isWallClimbing = false;
                Debug.Log("IsSlide222");
                _rb.drag = _stat.WallSlideSpeed;
            }
        }
        else
        {
            _isWallSliding = false;
            _rb.drag = 0;
            if (_collisionCtrl.OnGround) _holdCounter = _stat.WallHoldTime;
        }
        _rb.velocity = _velocity;
    }

    #endregion
    #region WallJump
    public bool IsCanWallJump => (_collisionCtrl.OnWallRight || _collisionCtrl.OnWallLeft) && !_collisionCtrl.OnGround;
    public void checkWallJump()
    {
        if (JumpCtrl._isJumping) { _isWallJumping = false; }
        else if (IsCanWallJump && PlayerCtrl.instance.JumpDown)
        {
            _isWallJumping = true;
            HandleWallJumping();
        }
    }
    public void HandleWallJumping()
    {
        float facingDirection = transform.parent.localScale.x > 0 ? 1f : -1f;
        Vector2 jumpDirection = Vector2.zero;
        if (_collisionCtrl.OnWallLeft)
        {
            jumpDirection = new Vector2(facingDirection * _wallJumpDirection.x, _wallJumpDirection.y);
        }
        else if (_collisionCtrl.OnWallRight)
        {
            jumpDirection = new Vector2(-facingDirection* _wallJumpDirection.x, _wallJumpDirection.y);
        }
        else
        {
            return;
        }

        jumpDirection *= _stat.WallJumpForce;

        if (Mathf.Sign(_rb.velocity.x) != Mathf.Sign(jumpDirection.x))
        {
            jumpDirection.x -= _rb.velocity.x;//Mathf.Max(jumpDirection.x - _rb.velocity.x,0);
        }
        if (_rb.velocity.y < 0)
        {
            jumpDirection.y = _rb.velocity.y; //Mathf.Max(jumpDirection.y - _rb.velocity.y, 0);
        }

        _rb.AddForce(jumpDirection, ForceMode2D.Impulse);
        _isWallSliding = false;
        StartCoroutine(DisableWallInteraction());
        StartCoroutine(ResetWallJump());
    }
    IEnumerator DisableWallInteraction()
    {
        _isWallJumping = true;
        yield return new WaitForSeconds(0.2f);
        _isWallJumping = false;
    }

    IEnumerator ResetWallJump()
    {
        yield return new WaitForSeconds(0.2f);
        _isWallJumping = false;
    }
    #endregion
}
