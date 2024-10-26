using System.Collections;
using UnityEditor;
using UnityEngine;

public class TestControl : MonoBehaviour
{
    #region Base
    [HideInInspector] protected UseableStats _stat;
    [HideInInspector] protected Rigidbody2D _rb;
    [HideInInspector] protected Animator _anim;
    [HideInInspector] protected Collider2D _col;
    [HideInInspector] private TrailRenderer _tr;

    protected virtual void LoadComponent()
    {
        _stat = AssetDatabase.LoadAssetAtPath<UseableStats>("Assets/ScriptableObject/_stats.asset");
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _col = GetComponent<Collider2D>();
        _tr = GetComponentInChildren<TrailRenderer>();
    }
    #endregion
    [Header("Config")]
    [SerializeField] float _time;
    [SerializeField] Vector2 _velocity;
    private void Awake()
    {
        LoadComponent();
        _isFacingRight = true;
        _rb.gravityScale = _stat.DefaultGravityScale;
        _holdCounter = _stat.WallHoldTime;
    }
    void Update()
    {
        _time += Time.deltaTime;
        _timeJumpWasPressed -= Time.deltaTime;
        if (PlayerCtrl.instance.JumpDown) OnJumpDown();
        if (PlayerCtrl.instance.JumpReleased) OnJumpReleased();
        JumpCheck();
        checkWallJump();
        DashOrder();
    }
    private void FixedUpdate()
    {
        CheckCollision();
        SetGravity();
        _velocity = _rb.velocity;
        SetFacingDirection(PlayerCtrl.instance.Move);

        HanldleMove(1);
        //JumpCheck();
        WallOrder();

    }

    #region Input
    void OnJumpDown()
    {
        _timeJumpWasPressed = _stat.JumpBufferTime;
    }
    void OnJumpReleased()
    {
        if (_rb.velocity.y > 0 && !_isJumpCutoffApplied && _isJumping)
            _rb.velocity *= new Vector2(1, _stat.JumpCutOffMultipiler);
        _isJumpCutoffApplied = true;
    }
    #endregion

    #region Collision
    [Header("Colliosion----------------------------")]
    [SerializeField] Transform _wallLeftPoint;
    [SerializeField] Transform _wallRightPoint;
    [SerializeField] Transform _groundPoint;

    [SerializeField] private bool checkedGround;
    [SerializeField] private bool checkedWallRight;
    [SerializeField] private bool checkedWallLeft;

    [SerializeField] private Color groundGizmoColor = Color.magenta;
    [SerializeField] private Color wallLeftGizmoColor = Color.yellow;
    [SerializeField] private Color wallRightGizmoColor = Color.red;

    private void CheckCollision()
    {

        //Vector2 colliderBottomPoint = new Vector2(_colFeet.bounds.center.x, _colFeet.bounds.min.y);
        checkedGround = Physics2D.OverlapBox(_groundPoint.position, _stat.GroundCheckSize, 0f, _stat.GroundLayer) != null;

        //Vector2 colliderLeftPoint = new Vector2(_colFeet.bounds.min.x, _colFeet.bounds.center.y);
        checkedWallLeft = Physics2D.OverlapBox(_wallLeftPoint.position, _stat.WallCheckSize, 0f, _stat.GroundLayer) != null;

        //Vector2 colliderRightPoint = new Vector2(_colFeet.bounds.max.x, _colFeet.bounds.center.y);
        checkedWallRight = Physics2D.OverlapBox(_wallRightPoint.position, _stat.WallCheckSize, 0f, _stat.GroundLayer) != null;
    }
    private void OnDrawGizmos()
    {
        if (_rb == null) return;
        Gizmos.color = groundGizmoColor;
        Gizmos.DrawWireCube(_groundPoint.position, _stat.GroundCheckSize);

        Gizmos.color = wallLeftGizmoColor;
        Gizmos.DrawWireCube(_wallLeftPoint.position, _stat.WallCheckSize);

        Gizmos.color = wallRightGizmoColor;
        Gizmos.DrawWireCube(_wallRightPoint.position, _stat.WallCheckSize);
    }
    #endregion

    #region Walking
    [Header("Move-------------------------")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _speedModifier, _maxSpeed, _speedChange;
    void HanldleMove(float learpAmount)
    {
        _maxSpeed = PlayerCtrl.instance.Move.x * _stat.WalkSpeed;

        _maxSpeed = Mathf.Lerp(_rb.velocity.x, _maxSpeed, learpAmount);

        _speedChange = _maxSpeed - _rb.velocity.x;

        _acceleration = (Mathf.Abs(_maxSpeed) > 0.01) && checkedGround ? _stat.Acceleration : _stat.Deceleration;

        _speedModifier = _speedChange * _acceleration;

        _rb.AddForce(_speedModifier * Vector2.right, ForceMode2D.Force);
    }
    #endregion

    #region Jumping
    [Header("Jump----------------")]
    public bool _isJumping;
    public bool _isFalling;

    [SerializeField] private float _timeJumpWasPressed, _timeLeftGround = float.MinValue;

    private bool _isJumpCutoffApplied;
    private float _jumpPower;
    private int _jumpLeft;

    public bool IsCanJump => checkedGround && !_isJumping;
    void JumpCheck()
    {
        if (checkedGround)
        {
            _timeLeftGround = _stat.JumpCoyoteTime;
            _isJumpCutoffApplied = false;
            _jumpLeft = _stat.JumpCount;
        }
        else
        {
            _timeLeftGround -= Time.fixedDeltaTime;
        }

        if (_isJumping && _rb.velocity.y < 0)
        {
            _isJumping = false;
            if (!_isWallJumping)
                _isFalling = true;
        }
        if (_isWallJumping)
        {
            _isWallJumping = false;
        }

        if (_timeLeftGround > 0 && !_isJumping && !_isWallJumping)
        {
            _isJumpCutoffApplied = false;
            if (!_isJumping)
            {
                _isFalling = false;
            }
        }
        if (!_isJumping
            && _timeJumpWasPressed > 0
            && (_timeLeftGround > 0 || _jumpLeft > 0))
        {
            _isJumping = true;
            _isFalling = false;
            _isWallJumping = false;

            if (_timeLeftGround <= 0) _jumpLeft--;
            HandleJumping();
        }
        else if (_isWallJumping)
        {
            _isJumping = false;
            _isFalling = false;
        }
    }

    void HandleJumping()
    {
        _timeJumpWasPressed = 0;
        _timeLeftGround = 0;

        _jumpPower = _stat.JumpForce;
        if (_rb.velocity.y < 0f)
        {
            _jumpPower -= _rb.velocity.y;
        }
        _rb.AddForce(_jumpPower * Vector2.up, ForceMode2D.Impulse);

    }
    #endregion

    #region Wall
    [Header("Wall----------------")]
    public bool _isWallClimbing;
    public bool _isWallJumping;
    public bool _isWallSliding;

    private Vector2 _wallJumpDirection = new Vector2(1, 1);
    [SerializeField] private float _holdCounter;

    #region WallSlide
    public bool IsCanWallSlide => !_isWallJumping && !checkedGround && (checkedWallRight || checkedWallLeft);
    public void WallOrder()
    {
        if (IsCanWallSlide)
        {
            _isWallSliding = true;
            Debug.Log(""+IsCanWallSlide);
            if (IsCanClimb) WallClimb();
            if (_isWallSliding) WallSlide();
        }
        else
        {
            _isWallSliding = false;
            _rb.drag = 0;
            if (checkedGround) _holdCounter = _stat.WallHoldTime;
        }
        _rb.velocity = _velocity;
    }
    void WallSlide()
    {
        if (checkedGround) _holdCounter = _stat.WallHoldTime;
        _isWallClimbing = false;
        _rb.drag = _stat.WallSlideSpeed;
    }

    #endregion
    #region WallCLimb
    bool IsCanClimb => PlayerCtrl.instance.ClimbDown && _holdCounter > 0;
    void WallClimb()
    {
        _isWallClimbing = true;
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
    #endregion
    #region WallJump
    public bool IsCanWallJump => (checkedWallRight || checkedWallLeft) && !checkedGround;
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
        float facingDirection = transform.localScale.x > 0 ? 1f : -1f;
        Vector2 jumpDirection = Vector2.zero;
        if (checkedWallLeft)
        {
            jumpDirection = new Vector2(facingDirection * _wallJumpDirection.x, _wallJumpDirection.y);
        }
        else if (checkedWallRight)
        {
            jumpDirection = new Vector2(-facingDirection * _wallJumpDirection.x, _wallJumpDirection.y);
        }
        else
        {
            return;
        }

        jumpDirection *= _stat.WallJumpForce;

        if (Mathf.Sign(_rb.velocity.x) != Mathf.Sign(jumpDirection.x))
        {
            jumpDirection.x -= _rb.velocity.x;
        }
        if (_rb.velocity.y < 0)
        {
            jumpDirection.y -= _rb.velocity.y;
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
    #endregion

    #region Dash
    [Header("Dash---------------")]
    public static bool _isDashing;

    private bool _isCanDash = true;

    private bool _dashReq;//for update

    private float _dashCounter;

    private Vector2 _dashDirection;
    
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
        if (checkedGround && _dashCounter <= 0.01f) _isCanDash = true;

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
    #endregion

    #region Others Check
    public bool _isFacingRight { get; private set; }
    public void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
            transform.localScale = new Vector2(1, 1);
        }
        else if (moveInput.x < 0 && _isFacingRight)
        {
            _isFacingRight = false;
            transform.localScale = new Vector2(-1, 1);
        }
    }
    private void SetGravity()
    {
        if (_isWallSliding) _rb.gravityScale = 1;
        else if (_isWallClimbing) _rb.gravityScale = 0;
        else if ((_isJumping || _isFalling))
        {
            _rb.gravityScale = _stat.JumpFallGravity;
        }
        else
        {
            _rb.gravityScale = _stat.DefaultGravityScale;
        }
    }
    #endregion
}