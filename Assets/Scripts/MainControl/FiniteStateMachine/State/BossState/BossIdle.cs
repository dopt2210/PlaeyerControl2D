using UnityEngine;

public class BossIdle : BossState
{
    private float idleTimer;
    private float idleTime;
    public BossIdle(Boss boss) : base(boss, "Idle", Boss.BossStateEnum.Idle)
    {
    }

    public override void AnimationTriggerEvent(Boss.BossStateEnum stateEnum) { }

    public override void EnterState()
    {
        idleTime = 2f;
        idleTimer = 0f;

        boss.SetAnimation(anim, true);
    }

    public override void ExitState()
    {
        boss.SetAnimation(anim, false); 
        
        boss._animationTime = Mathf.Max(0.5f, boss._animationTime - 0.1f);
    }

    public override void LogicUpdate()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTime)
        {
            boss.stateMachine.ChangeState(Boss.BossStateEnum.Attack);
        }
    }

    public override void PhysicsUpdate() { }
}
