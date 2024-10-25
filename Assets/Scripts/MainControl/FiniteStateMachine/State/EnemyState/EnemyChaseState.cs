using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState<Enemy.EnemyStateEnum>
{
    Enemy enemy;
    public EnemyChaseState(Enemy enemy) : base(Enemy.EnemyStateEnum.Chase)
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
        Debug.Log("Chasing");
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (enemy._isAttack == true && enemy._isAggro == true)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Attack);
        }
        else if (enemy._isAggro == false) 
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Idle);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
