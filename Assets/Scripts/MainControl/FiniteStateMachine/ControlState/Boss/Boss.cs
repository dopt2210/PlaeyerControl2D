using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : BaseMovement
{
    public StateMachine<BossStateEnum> stateMachine;
    public EnemyData enemyData;
    public float _animationTime;
    private bool isDead;
    public static bool IsBossDie { get; private set; }

    public Image timeBar;
    public List<GameObject> attackList;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private int poolSize = 10;

    public float totalTime = 100f;
    [SerializeField] private float _time;
    public int maxSpawnCount = 1;
    private float timeThreshold = 10f;

    private void OnEnable()
    {
        isDead = false;
        _time = totalTime;
        maxSpawnCount = 1;
        timeBar.transform.parent.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        timeBar.transform.parent.gameObject.SetActive(false);
        StopAllCoroutines();
    }
    private void OnDestroy()
    {
        IsBossDie = isDead;
    }
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
        stateMachine.States.Add(BossStateEnum.Die, new BossDie(this));

        InitObjectPool();
        LoadComponents();
        LoadRangeTriggers();
        
    }
    private void Start()
    {
        stateMachine.InitState(stateMachine.States[BossStateEnum.Idle]);
        LoadBoss();

    }
    private void Update()
    {
        if (stateMachine == null) { Debug.Log("Null Machine"); return; }

        _time -= Time.deltaTime;

        if (_time % timeThreshold <= Time.deltaTime)
        {
            maxSpawnCount = Mathf.Min(maxSpawnCount + 1, 10);
        }

        if (_time <= 0f)
        {
            stateMachine.ChangeState(BossStateEnum.Die);
            Destroy(gameObject);
            isDead = true;
            return;
        }

        timeBar.fillAmount = _time / totalTime;
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
            AddToPool(attackList[i]);
        }
    }
    private void AddToPool(GameObject prefab)
    {
        if (prefab == null) return;

        GameObject obj = Instantiate(prefab, this.transform);
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
        if (obj != null)
        {
            obj.SetActive(false); 
            if (!pool.Contains(obj))
            {
                pool.Add(obj); 
            }
        }
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
