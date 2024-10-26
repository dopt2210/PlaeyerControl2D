using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class _PlayerStateMachine : StateManager<_PlayerStateMachine.PlayerStateEnum>
{
    public enum PlayerStateEnum
    {
        Idle,
        Run,
        Jump,
        Dash,
        WallClimb,
        WallSlide,
        Fall,
        Ledge
    }

    private void Awake()
    {
        States.Add(PlayerStateEnum.Idle, new _IdleState(this));
        States.Add(PlayerStateEnum.Run, new _RunState(this));
        //States[PlayerStateEnum.Idle] = new _IdleState(this);
        //States[PlayerStateEnum.Run] = new _RunState(this);

        CurrentState = States[PlayerStateEnum.Idle];
        LoadComponents();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    private void FixedUpdate()
    {
        SetFacingDirection(PlayerCtrl.instance.Move);
        HanldleMove(1);
        CheckCollision();
    }
    #region Move
    [SerializeField] private float _acceleration, _speedModifier, _maxSpeed, _speedChange;

    public void HanldleMove(float learpAmount)
    {

        _maxSpeed = PlayerCtrl.instance.MoveX * _stat.WalkSpeed;

        _maxSpeed = Mathf.Lerp(_rigidbody2d.velocity.x, _maxSpeed, learpAmount);

        _speedChange = _maxSpeed - _rigidbody2d.velocity.x;

        _acceleration = (Mathf.Abs(_maxSpeed) > 0.01) ? _stat.Acceleration : _stat.Deceleration;

        _speedModifier = _speedChange * _acceleration;

        _rigidbody2d.AddForce(_speedModifier * Vector2.right, ForceMode2D.Force);
    }
    #endregion
    #region Jump
    protected float _timeJumpWasPressed, _timeLeftGround = float.MinValue;
    protected bool _isJumpCutoffApplied;
    protected int _jumpLeft;
    public void OnJumpDown()
    {
        _timeJumpWasPressed = _stat.JumpBufferTime;
    }
    public void OnJumpReleased()
    {
        if (_rigidbody2d.velocity.y > 0 && !_isJumpCutoffApplied)
            _rigidbody2d.velocity *= new Vector2(1, _stat.JumpCutOffMultipiler);
        _isJumpCutoffApplied = true;
    }
    public void HandleJumping()
    {
        var _jumpPower = _stat.JumpForce;
        if (_rigidbody2d.velocity.y < 0f)
        {
            //_jumpPower = Mathf.Max(_jumpPower - _rb.velocity.y, 0f);
            _jumpPower -= _rigidbody2d.velocity.y;
        }
        _rigidbody2d.AddForce(_jumpPower * Vector2.up, ForceMode2D.Impulse);

    }
    #endregion
    #region CollisionCheck
    public Transform GroundCheckPoint;
    public Transform WallLeftCheckPoint;
    public Transform WallRightCheckPoint;

    [SerializeField] private bool checkedGround;
    [SerializeField] private bool checkedWallRight;
    [SerializeField] private bool checkedWallLeft;

    public bool OnGround { get => checkedGround; private set { } }
    public bool OnWallRight { get => checkedWallRight; private set { } }
    public bool OnWallLeft { get => checkedWallLeft; private set { } }
    private void CheckCollision()
    {
        checkedGround = Physics2D.OverlapBox(GroundCheckPoint.position, _stat.GroundCheckSize, 0f, _stat.GroundLayer) != null;

        checkedWallLeft = Physics2D.OverlapBox(WallLeftCheckPoint.position, _stat.WallCheckSize, 0f, _stat.GroundLayer) != null;

        checkedWallRight = Physics2D.OverlapBox(WallRightCheckPoint.position, _stat.WallCheckSize, 0f, _stat.GroundLayer) != null;
    }
    #endregion
    #region Others
    [SerializeField] public Rigidbody2D _rigidbody2d;
    [SerializeField] public UseableStats _stat {  get; private set; }
    [SerializeField] public Collider2D _collider;
    [SerializeField] public Animator _animator;
    protected virtual void LoadComponents()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _stat = AssetDatabase.LoadAssetAtPath<UseableStats>("Assets/ScriptableObject/_stats.asset");
        _isFacingRight = true;
    }

    public virtual void Validation()
    {
        Assert.IsNotNull(_rigidbody2d, "Rigidbody is not assigned");
        Assert.IsNotNull(_rigidbody2d, "ScriptableObject is not assigned");
        Assert.IsNotNull(_rigidbody2d, "Collider is not assigned");
    }

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
    #endregion
}
