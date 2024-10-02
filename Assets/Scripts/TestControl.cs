using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TestControl : MonoBehaviour
{
    [SerializeField] float _time;
    [SerializeField] UseableStats _stat;
    [SerializeField] Vector2 _velocity;
    private Rigidbody2D _rb;
    private Collider2D _col;
    private float DefaultGravityScale = 10;
    private void OnEnable()
    {
        _rb.gravityScale = DefaultGravityScale;
        holdCounter = _stat.WallHoldTime;
    }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }
    void Update()
    {
        _time += Time.deltaTime;
        GatherInput();

        DashOrder();
    }
    private void FixedUpdate()
    {
        CheckCollision();
        _velocity = _rb.velocity;

        HandleWalking();
        JumpOrder();
        WallOrder();
        HandleWallJumping();

    }

    #region Input
    InputField _input;
    private bool _jumpReq, _climbReq, _jumpWallReq, _dashReq;
    void GatherInput()
    {
        _input = new InputField
        {
            JumpDown = Input.GetButtonDown("Jump"),
            JumpHeld = Input.GetButton("Jump"),
            ClimbDown = Input.GetKey(KeyCode.R),
            DashDown = Input.GetKeyDown(KeyCode.E),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
        };
        if (_input.JumpDown) _jumpReq = true;
        //if (!OnGround && _input.ClimbDown) _climbReq = true;
        if ((isWallRight || isWallLeft) && !isGrounded && _input.JumpDown) _jumpWallReq = true;
        if (_input.DashDown && isDash) _dashReq = true;
    }
    #endregion

    #region Collision

    private bool isGrounded;
    private bool isWallRight;
    private bool isWallLeft;
    private bool isCeiling;

    private Vector2 boxSize;
    private float grounDistance = 0.1f;
    private float boxOffsetX = 0.5f;
    //private float boxOffsetY = 0.5f;

    void CheckCollision()
    {
        boxSize = new Vector2(_col.bounds.size.x, grounDistance);
        //Vector2 colliderBottom = new Vector2(tranform.position.x, transform.position.y-boxOffsetY);
        Vector2 colliderBottom = new Vector2(_col.bounds.center.x, _col.bounds.min.y);
        isGrounded = Physics2D.OverlapBox(colliderBottom, boxSize, 0f, _stat.GroundLayer) != null;
        //OnGround = Physics2D.Raycast(colliderBottom, Vector2.down, groundCheckDistance, GroundLayer);

        Vector2 colliderTop = new Vector2(_col.bounds.center.x, _col.bounds.max.y);
        isCeiling = Physics2D.OverlapBox(colliderTop, boxSize, 0f, _stat.GroundLayer) != null;

        Vector2 colliderLeft = new Vector2(transform.position.x - boxOffsetX, transform.position.y);
        isWallLeft = Physics2D.OverlapBox(colliderLeft, boxSize, 0f, _stat.GroundLayer) != null;

        Vector2 colliderRight = new Vector2(transform.position.x + boxOffsetX, transform.position.y);
        isWallRight = Physics2D.OverlapBox(colliderRight, boxSize, 0f, _stat.GroundLayer) != null;
    }
    #endregion

    #region Walking
    [SerializeField] private float Acceleration, MaxSpeed, SpeedModifier;
    void HandleWalking()
    {
        Acceleration = isGrounded ? _stat.GroundAcceleration : _stat.AirAcceleration;

        MaxSpeed = Acceleration * Time.deltaTime;

        SpeedModifier = _input.Move.x * _stat.WalkSpeed;

        _velocity.x = Mathf.MoveTowards(_rb.velocity.x, SpeedModifier, MaxSpeed);

        _rb.velocity = _velocity;

    }
    #endregion

    #region Jumping
    private float timeJumpWasPressed, timeLeftGround = float.MinValue;
    private bool isCoyoteTime;

    [SerializeField] float JumpPower;
    private int JumpLeft;

    //private bool isJumping;
    private bool jumpCutoffApplied;

    void JumpOrder()
    {
        // Coyote time
        if (isGrounded && _rb.velocity.y == 0)
        {
            JumpLeft = 0;
            timeLeftGround = _stat.CoyoteTime;
            isCoyoteTime = false;

            //isJumping = false;
            jumpCutoffApplied = false;
        }
        else
        {
            timeLeftGround -= Time.deltaTime;
        }

        if (_jumpReq)
        {
            _jumpReq = false;
            HandleJumping();
        }

        if (!_input.JumpHeld && _rb.velocity.y > 0 && !jumpCutoffApplied)
        {
            _velocity.y *= _stat.JumpCutOff;
            jumpCutoffApplied = true;
        }

        _rb.velocity = _velocity;
    }

    void HandleJumping()
    {
        if (timeLeftGround > 0 || (JumpLeft < _stat.JumpCount && isCoyoteTime))
        {
            timeLeftGround = 0;
            isCoyoteTime = true;

            if (isCoyoteTime)
                JumpLeft++;
            JumpPower = Mathf.Sqrt(_stat.JumpHeight * (Physics2D.gravity.y * _rb.gravityScale) * -2) * _rb.mass;
            if (_velocity.y > 0f)
            {
                JumpPower = Mathf.Max(JumpPower - _velocity.y, 0f);
            }
            _velocity.y = JumpPower;
            //isJumping = true;
        }
    }
    #endregion

    #region Wall
    [SerializeField] private float holdCounter;
    void WallOrder()
    {
        if (isWallLeft || isWallRight)
        {
            if (_input.ClimbDown && holdCounter > 0)
            {
                if (_input.Move.y > 0)
                {
                    _velocity.y = _stat.WallSlideSpeed;
                    _rb.gravityScale = 0;
                }
                if (_input.Move.y < 0)
                {
                    _velocity.y = -_stat.WallSlideSpeed;
                    _rb.gravityScale = 0;
                }
                if (_input.Move.y == 0)
                {
                    _velocity.y = 0;
                    _rb.gravityScale = 0;
                }
                holdCounter -= Time.fixedDeltaTime;
            }
            else
            {
                _rb.drag = 0;
                _rb.gravityScale = DefaultGravityScale;
            }
        }
        else
        {
            _rb.drag = 0;
            _rb.gravityScale = 10;
            holdCounter = _stat.WallHoldTime;
        } 

        _rb.velocity = _velocity;
    }


    private Vector2 wallJumpDirection = new Vector2(1, 1);
    void HandleWallJumping()
    {
        if (_jumpWallReq)
        {
            _jumpWallReq = false;
            Vector2 jumpDirection;
            if (isWallLeft)
            {
                jumpDirection = new Vector2(wallJumpDirection.x, wallJumpDirection.y);
            }
            else if (isWallRight)
            {
                jumpDirection = new Vector2(-wallJumpDirection.x, wallJumpDirection.y);
            }
            else
            {
                return;
            }

            _velocity = jumpDirection * _stat.WallJumpForce;

            Debug.Log("HasJump");
        }

        _rb.velocity = _velocity;
    }


    #endregion

    #region Dash
    private Vector2 dashDirection;
    private bool isDash = true;
    private bool isDashing;
    private float dashCounter;

    private void DashOrder()
    {
        if (_dashReq)
        {
            _dashReq = false;
            isDashing = true;
            isDash = false;

            dashCounter = _stat.DashCooldown;

            dashDirection = new Vector2(_input.Move.x, _input.Move.y).normalized;
            if (dashDirection == Vector2.zero)
            {
                dashDirection = new Vector2(transform.localScale.x, 0f);
            }

            StartCoroutine(HandleDash());
        }

        if (isDashing)
        {
            //_rb.velocity = _dashDirection * _stat.dashSpeed;
            _rb.MovePosition(_rb.position + dashDirection * _stat.DashSpeed);
            return;
        }
        if (isGrounded && dashCounter <= 0.01f) isDash = true;

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
    #endregion

    [SerializeField] bool isFacingRight = false;
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }

    public struct InputField
    {
        public bool JumpDown;
        public bool JumpRelease;
        public bool JumpHeld;
        public bool ClimbDown;
        public bool DashDown;
        public Vector2 Move;
    }
}
