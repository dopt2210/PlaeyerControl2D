using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine StateMachine{  get; private set; }
    public IdleState IdleState{ get; private set; }
    public RunState RunState{ get; private set; }
    public JumpState JumpState{ get; private set; }
    public LandState LandState{ get; private set; }
    public AirState AirState{ get; private set; }
    #endregion
    #region Base Variables
    public Animator _anim{  get; private set; }
    public Rigidbody2D _rb{ get; private set; }
    
    public Vector2 _velocity;
    private PlayerData _playerData;
    #endregion
    #region Collision Variables
    public Transform GroundCheckPoint;
    public Transform WallLeftCheckPoint;
    public Transform WallRightCheckPoint;

    public bool OnGround { get => checkedGround; private set { } }
    public bool OnWallRight { get => checkedWallRight; private set { } }
    public bool OnWallLeft { get => checkedWallLeft; private set { } }

    [SerializeField] private bool checkedGround;
    [SerializeField] private bool checkedWallRight;
    [SerializeField] private bool checkedWallLeft;

    #endregion

    private void Reset()
    {
        LoadComponents();
    }
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new IdleState(this, StateMachine, _playerData, "Idle");
        RunState = new RunState(this, StateMachine, _playerData, "Run");
        JumpState = new JumpState(this, StateMachine, _playerData, "InAir");
        AirState = new AirState(this, StateMachine, _playerData, "InAir");
        LandState = new LandState(this, StateMachine, _playerData, "Land");
        LoadComponents();
    }
    private void Start()
    {
        StateMachine.InitState(IdleState);
    }
    private void Update()
    {
        if (StateMachine == null) { Debug.Log("Null Machine"); return;  }
        StateMachine.CurrentState.LogicUpdate();

        SetFacingDirection(PlayerCtrl.instance.Move);
    }
    private void FixedUpdate()
    {
        if (StateMachine == null) { Debug.Log("Null Machine"); return; }
        StateMachine.CurrentState.PhysicsUpdate();

        this._velocity = _rb.velocity;
        CheckCollision();
    }
    #region Check Collision
    private void CheckCollision()
    {
        checkedGround = Physics2D.OverlapBox(GroundCheckPoint.position, _playerData.GroundCheckSize, 0f, _playerData.GroundLayer) != null;

        checkedWallLeft = Physics2D.OverlapBox(WallLeftCheckPoint.position, _playerData.WallCheckSize, 0f, _playerData.GroundLayer) != null;

        checkedWallRight = Physics2D.OverlapBox(WallRightCheckPoint.position, _playerData.WallCheckSize, 0f, _playerData.GroundLayer) != null;
    }
    #endregion
    #region Move
    [SerializeField] private float _acceleration, _speedModifier, _maxSpeed, _speedChange;

    public void HanldleMove(float learpAmount)
    {

        _maxSpeed = PlayerCtrl.instance.MoveX * _playerData.WalkSpeed;

        _maxSpeed = Mathf.Lerp(_rb.velocity.x, _maxSpeed, learpAmount);

        _speedChange = _maxSpeed - _rb.velocity.x;

        _acceleration = (Mathf.Abs(_maxSpeed) > 0.01) ? _playerData.Acceleration : _playerData.Deceleration;

        _speedModifier = _speedChange * _acceleration;

        _rb.AddForce(_speedModifier * Vector2.right, ForceMode2D.Force);
    }
    #endregion
    #region Jump
    public void HandleJumping()
    {
        var _jumpPower = _playerData.JumpForce;
        if (_rb.velocity.y < 0f)
        {
            //_jumpPower = Mathf.Max(_jumpPower - _rb.velocity.y, 0f);
            _jumpPower -= _rb.velocity.y;
        }
        _rb.AddForce(_jumpPower * Vector2.up, ForceMode2D.Impulse);

    }
    #endregion
    #region Others
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationTriggerEnd() => StateMachine?.CurrentState.AnimationTriggerEnd();
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
    private void LoadComponents()
    {
        _playerData = AssetDatabase.LoadAssetAtPath<PlayerData>("Assets/ScriptableObject/NewDataPlayer.asset");
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _isFacingRight = true;
        _rb.gravityScale = _playerData.DefaultGravityScale;
    }
    #endregion

}
