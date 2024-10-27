using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseMovement, IDamageAble<object>, IMoveAble
{
    public StateMachine<EnemyStateEnum> stateMachine;
    public EnemyData enemyData;
    public enum EnemyStateEnum
    {
        Idle,
        Attack,
        Chase
    }
    #region Vars
    public bool _isFacingRight { get; set; } = true;
    public bool _isAggro { get; private set; }
    public bool _isAttack { get; private set; }
    public float _maxHP {  get; set; }
    public float _currentHP {  get; set; }
    public int _deadCount { get; set; }

    #endregion
    protected override void Awake()
    {
        stateMachine = new StateMachine<EnemyStateEnum>();

        stateMachine.States.Add(EnemyStateEnum.Idle, new EnenmyIdleState(this));
        stateMachine.States.Add(EnemyStateEnum.Chase, new EnemyChaseState(this));
        stateMachine.States.Add(EnemyStateEnum.Attack, new EnemyAttackState(this));
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
    protected override void LoadComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }

    #region Move Enemy
    public void HandleMove(float direction)
    {

        if (_collisionCtrl.OnGround)
        {
            _rb.velocity = new Vector2(direction, _rb.velocity.y);
            SetFacingDirection(direction * Vector2.one);
        }
    }
    public void SetFacingDirection(Vector2 direction)
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
    #region Other
    public bool SetAggro(bool isArggo) => _isAggro = isArggo;
    public bool SetAttack(bool isAttack) => _isAttack = isAttack;
    public void SetAnimation(string anim, bool status)
    {
        if (_anim == null) { Debug.Log("null anim"); return; }
        _anim.SetBool(anim, status);
    }
    public void AnimationTriggerEvent(EnemyStateEnum enemyState)
    {
        stateMachine.currentState.AnimationTriggerEvent(enemyState);
    }

    #endregion
    #region Interface Function
    public void Damage(float dmg, IEnumerable<object> objects = null)
    {
        _currentHP -= dmg;
        if (_currentHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        throw new System.NotImplementedException();
    }
    public void Respawn()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
