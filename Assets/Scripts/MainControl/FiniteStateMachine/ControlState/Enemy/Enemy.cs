using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BaseMovement, IDamageAble<object>, IMoveAble
{
    public StateMachine<EnemyStateEnum> stateMachine;
    public EnemyData enemyData;
    public Rigidbody2D arrowPrefab;
    public void DestroyArrow(Rigidbody2D prefab, float time)
    {
        Destroy(prefab, time);
    }

    public enum EnemyStateEnum
    {
        Idle,
        Attack,
        Chase,
        Shot
    }
    #region Interface Vars
    public bool _isFacingRight { get; set; } = true;
    public bool _isAggro { get; private set; }
    public bool _isAttack { get; private set; }
    public bool _isShot {  get; private set; }
    public float _maxHP { get; set; }
    public float _currentHP { get; set; }
    public int _deadCount { get; set; }
    public bool _isCanMove { get; set; }

    #endregion
    protected override void Awake()
    {
        stateMachine = new StateMachine<EnemyStateEnum>();

        stateMachine.States.Add(EnemyStateEnum.Idle, new EnenmyIdleState(this));
        stateMachine.States.Add(EnemyStateEnum.Chase, new EnemyChaseState(this));
        stateMachine.States.Add(EnemyStateEnum.Attack, new EnemyAttackState(this));
        stateMachine.States.Add(EnemyStateEnum.Shot, new EnemyShotState(this));
        LoadComponents();
    }

    private void Start()
    {
        stateMachine.InitState(stateMachine.States[EnemyStateEnum.Idle]);
        _currentHP = _maxHP;
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

    protected virtual void LoadRangeTriggers() { }

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
    public virtual bool SetAggro(bool isArggo) => _isAggro = isArggo;
    public virtual bool SetAttack(bool isAttack) => _isAttack = isAttack;
    public virtual bool SetShot(bool isShot) => _isShot = isShot;
    public virtual bool CheckWall() => _collisionCtrl.OnWallRight;

    public virtual void SetAnimation(string anim, bool status)
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
