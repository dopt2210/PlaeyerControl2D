using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtackState : EnemyState<Enemy.EnemyStateEnum>
{
    Enemy enemy;
    public EnemyAtackState(Enemy enemy) : base(Enemy.EnemyStateEnum.Attack)
    {
        this.enemy = enemy;
    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum enemyStateEnum)
    {
        base.AnimationTriggerEvent(enemyStateEnum);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Attacking");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy._isAttack == false)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Chase);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
