using System;

public abstract class BaseState<EState> where EState : Enum
{
    protected EState stateKey;
    public BaseState(EState stateKey)
    {
        this.stateKey = stateKey;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void AnimationTriggerEvent(EState stateEnum);
}
