using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Boss : BaseMovement
{
    public StateMachine<BossStateEnum> stateMachine;
    public EnemyData enemyData;
    public float _animationTime = 2f;

    public GameObject attackPrefab1;
    public GameObject attackPrefab2;
    public GameObject attackPrefab3;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private int poolSize = 5;


    public enum BossStateEnum
    {
        Idle,
        Attack,
        Die
    }
    private void Awake()
    {
        stateMachine = new StateMachine<BossStateEnum>();

        stateMachine.States.Add(BossStateEnum.Idle, new BossIdle(this));
        stateMachine.States.Add(BossStateEnum.Attack, new BossAttack(this));
        //stateMachine.States.Add(BossStateEnum.Die, new BossDie(this));
        
        InitObjectPool();
    }
    private void Start()
    {
        stateMachine.InitState(stateMachine.States[BossStateEnum.Idle]);
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
    private void InitObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddToPool(attackPrefab1);
            AddToPool(attackPrefab2);
            AddToPool(attackPrefab3);
        }
    }
    private void AddToPool(GameObject prefab)
    {
        if (prefab == null) return;

        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
    }
    public GameObject GetFromPool(int random)
    {
        for(int i = 0; i < poolSize;i++)
        {
            if (!pool[i].activeInHierarchy && i == random)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        return null; 
    }
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
    protected override void LoadComponents()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collisionCtrl = GetComponent<CollisionCtrl>();
    }

    protected virtual void LoadBoss() { }
    protected virtual void LoadRangeTriggers() { }
    public virtual void AnimationTriggerEvent(BossStateEnum bossState)
    {
        stateMachine.currentState.AnimationTriggerEvent(bossState);
    }
    public virtual void SetAnimation(string anim, bool status)
    {
        if (_anim == null) { Debug.Log("not found animation"); return; } 
        _anim.SetBool(anim, status);
    }

}
