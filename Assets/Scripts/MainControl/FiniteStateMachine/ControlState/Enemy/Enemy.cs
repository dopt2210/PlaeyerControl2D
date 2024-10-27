using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageAble, IMoveAble
{
    public StateMachine<EnemyStateEnum> stateMachine;
    public enum EnemyStateEnum
    {
        Idle,
        Attack,
        Chase
    }
    #region Vars
    public Animator _anim {  get; set; }
    public Rigidbody2D _rb { get; set; }
    public CollisionCtrl _collisionCtrl { get; set; }
    public float _maxHP { get; set; }
    public float _currentHP { get; set; }
    public bool _isFacingRight { get; set; } = true;

    public float _randomSpeed = 1f;
    public float _randomRange = 5f;
    public bool _isAggro { get; private set; }
    public bool _isAttack { get; private set; }
    #endregion
    protected void Awake()
    {
        stateMachine = new StateMachine<EnemyStateEnum>();

        stateMachine.States.Add(EnemyStateEnum.Idle, new EnenmyIdleState(this));
        stateMachine.States.Add(EnemyStateEnum.Chase, new EnemyChaseState(this));
        stateMachine.States.Add(EnemyStateEnum.Attack, new EnemyAtackState(this));
    }

    private void Start()
    {
        stateMachine.InitState(stateMachine.States[EnemyStateEnum.Idle]);

        _currentHP = _maxHP;
        LoadComponents();

    }
    private void Update()
    {
        if (stateMachine == null) { Debug.Log("Null Machine"); return; }
        stateMachine.currentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        if (stateMachine == null) { Debug.Log("Null Machine"); return; }
        stateMachine.currentState.PhysicsUpdate();
    }
    protected void LoadComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }
    #region Action
    public void Damage(float dmg)
    {
        _currentHP -= dmg;
        if (_currentHP <= 0)
        {
            Died();
        }
    }

    public void Died()
    {
        throw new System.NotImplementedException();
    }

    #endregion
    #region MoveEnemy
    public void HandleMove(Vector2 velocity)
    {
        if (_collisionCtrl.OnGround)
        {
            _rb.velocity = new Vector2(velocity.x, _rb.velocity.y);
            SetFacingRight(velocity);
        }
    }
    public void SetFacingRight(Vector2 direction)
    {
        if (direction.x > 0 && !_isFacingRight)
        {
            _isFacingRight = true;
            transform.localScale = new Vector2(1, 1);
        }
        else if (direction.x < 0 && _isFacingRight)
        {
            _isFacingRight = false;
            transform.localScale = new Vector2(-1, 1);
        }
    }
    #endregion
    #region Others
    public bool SetAggro(bool isArggo) => _isAggro = isArggo;
    public bool SetAttack(bool isAttack) => _isAttack = isAttack;
    public void AnimationTriggerEvent(EnemyStateEnum enemyState)
    {
        stateMachine.currentState.AnimationTriggerEvent(enemyState);
    }
    #endregion
}
