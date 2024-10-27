using System;

public abstract class EnemyState<EState> where EState : Enum
{
    protected EState stateKey;

    public EnemyState(EState stateKey)
    {
        this.stateKey = stateKey;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void AnimationTriggerEvent(EState stateEnum);
}
