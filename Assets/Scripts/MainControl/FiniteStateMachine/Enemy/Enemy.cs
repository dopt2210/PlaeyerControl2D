using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageAble, IMoveAble
{
    public EnemyStateMachine<EnemyStateEnum> stateMachine;
    public enum EnemyStateEnum
    {
        Idle,
        Attack,
        Chase
    }
    #region Vars
    public float _maxHP { get; set; }
    public float _currentHP { get; set; }
    public Rigidbody2D _rb { get; set; }
    public bool _isFacingRight { get; set; } = true;
    public float _randomSpeed = 1f;
    public float _randomRange = 5f;
    public bool _isAggro { get; private set; }
    public bool _isAttack { get; private set; }
    #endregion
    private void Awake()
    {
        stateMachine = new EnemyStateMachine<EnemyStateEnum>();

        stateMachine.States.Add(EnemyStateEnum.Idle, new EnenmyIdleState(this));
        stateMachine.States.Add(EnemyStateEnum.Chase, new EnemyChaseState(this));
        stateMachine.States.Add(EnemyStateEnum.Attack, new EnemyAtackState(this));
    }

    private void Start()
    {
        stateMachine.InitState(stateMachine.States[EnemyStateEnum.Idle]);

        _currentHP = _maxHP;
        _rb = GetComponent<Rigidbody2D>();
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

    public bool SetAggro(bool isArggo) => _isAggro = isArggo;
    public bool SetAttack(bool isAttack) => _isAttack = isAttack;
    #endregion 
    #region MoveEnemy
    public void MoveEnemy(Vector2 velocity)
    {
        _rb.velocity = velocity;
        SetFacingRight(velocity);
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
    public void AnimationTriggerEvent(EnemyStateEnum enemyState)
    {
        stateMachine.currentState.AnimationTriggerEvent(enemyState);
    }
    #endregion
}
