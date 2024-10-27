using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : EnemyState<Enemy.EnemyStateEnum>
{
    public EnemyState(Enemy.EnemyStateEnum stateKey) : base(stateKey)
    {
    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum stateEnum)
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void LogicUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void PhysicsUpdate()
    {
        throw new System.NotImplementedException();
    }
}
