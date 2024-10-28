using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState<Enemy.EnemyStateEnum>
{
    Enemy enemy;
    Transform player;
    float moveSpeed = 1.7f;
    public EnemyChaseState(Enemy enemy) : base(Enemy.EnemyStateEnum.Chase)
    {
        this.enemy = enemy;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum enemyStateEnum) { }

    public override void EnterState()
    {
        enemy._anim.SetBool("Move", true);
    }

    public override void ExitState()
    {
        enemy._anim.SetBool("Move", false);
    }

    public override void LogicUpdate()
    {
        Vector2 moveDirection = (player.position - enemy.transform.position).normalized;
        enemy.HandleMove(moveDirection * moveSpeed);

        if (enemy._isAttack == true)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Attack);
        }
        else if (enemy._isAggro == false)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Idle);
        }
    }

    public override void PhysicsUpdate() { }
}
