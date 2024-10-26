using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState<EState> where EState : Enum
{
    protected EState stateKey;

    public EnemyState(EState stateKey)
    {
        this.stateKey = stateKey;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(EState enemyStateEnum) { }
}
