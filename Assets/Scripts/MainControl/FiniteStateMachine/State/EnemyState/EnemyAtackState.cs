using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtackState : EnemyState<Enemy.EnemyStateEnum>
{
    Enemy enemy;
    Transform player;
    public EnemyAtackState(Enemy enemy) : base(Enemy.EnemyStateEnum.Attack)
    {
        this.enemy = enemy;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum enemyStateEnum)
    {
        base.AnimationTriggerEvent(enemyStateEnum);
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy._anim.SetBool("Attack", true);
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy._anim.SetBool("Attack", false);
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
