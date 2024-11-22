using System.Collections;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    Transform player;
    private Coroutine Coroutine;
    public EnemyAttackState(Enemy enemy) : base(enemy, "Attack", Enemy.EnemyStateEnum.Attack)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum enemyStateEnum) { }

    public override void EnterState() { enemy.SetAnimation(anim, true); }

    public override void ExitState() { enemy.SetAnimation(anim, false); }

    public override void LogicUpdate()
    {

        if (enemy._isAttack == false)
        {
            enemy.DelayAnimation();
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Chase);
        }
    }

    public override void PhysicsUpdate() { }
    
}
