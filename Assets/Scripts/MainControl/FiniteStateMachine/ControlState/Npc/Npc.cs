using UnityEngine;

public class Npc : BaseMovement, IInteractable
{
    public StateMachine<NpcStateEnum> stateMachine;
    public enum NpcStateEnum
    {
        Idle,
        Talk
    }

    public bool _isTalkable {  get; private set; }
    public GameObject PlayerGameObject {  get; set; }
    public bool IsInteractable {  get => PlayerCtrl.instance.InteractDown; set { } }
    #region Base
    private void Awake()
    {
        stateMachine = new StateMachine<NpcStateEnum>();

        stateMachine.States.Add(NpcStateEnum.Idle, new NpcIdleState(this));
        stateMachine.States.Add(NpcStateEnum.Talk, new NpcTalkState(this));

        LoadComponents();   
    }
    private void Start()
    {
        stateMachine.InitState(stateMachine.States[NpcStateEnum.Idle]);
        LoadRangeTriggers();
    }
    private void Update()
    {
        if (stateMachine == null) { Debug.Log("Null Machine"); return; }
        stateMachine.currentState.LogicUpdate();
        LoadInteractImage();
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
    }
    #endregion
    protected virtual void LoadInteractImage() { }
    protected virtual void LoadRangeTriggers() { }
    public virtual void Interact() { }
    public virtual void DisableInteract() { }
    public virtual void SetRangeInteract(bool value) => _isTalkable = value;
    public virtual void SetAnimation(string anim, bool status)
    {
        if (_anim == null) { Debug.Log("null anim"); return; }
        _anim.SetBool(anim, status);
    }
    public void AnimationTriggerEvent(NpcStateEnum npcState)
    {
        stateMachine.currentState.AnimationTriggerEvent(npcState);
    }

}
