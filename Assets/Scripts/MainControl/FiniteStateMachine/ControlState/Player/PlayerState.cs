using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected PlayerState playerState;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected float startTime;
    protected bool animationEnd;
    private string animBool;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBool)
    {
        this.player = player;
        this.playerData = playerData;
        this.stateMachine = playerStateMachine;
        this.animBool = animBool;
    }

    public virtual void EnterState()
    {
        Checks();
        player._anim.SetBool(animBool, true);
        startTime = Time.time;
        animationEnd = false;
    }
    public virtual void ExitState()
    {
        player._anim.SetBool(animBool, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        Checks();
    }
    public virtual void Checks()
    {

    }
    public virtual void AnimationTrigger()
    {

    }
    public virtual void AnimationTriggerEnd() => animationEnd = true;

}
