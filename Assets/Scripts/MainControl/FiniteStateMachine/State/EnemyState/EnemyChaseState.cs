using UnityEngine;

public class EnemyChaseState : EnemyState
{
    Transform player;
    public EnemyChaseState(Enemy enemy) : base(enemy, "Chase", Enemy.EnemyStateEnum.Chase)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum enemyStateEnum) { }

    public override void EnterState() { enemy.SetAnimation(anim, true); }

    public override void ExitState() { enemy.SetAnimation(anim, false); }

    public override void LogicUpdate()
    {
        float moveDirection = player.position.x - enemy.transform.position.x;
        enemy.HandleMove(moveDirection * enemy.enemyData.SpeedChase);

        if (enemy._isShot && !enemy._isAttack)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Shot);
        }
        else if (enemy._isAttack)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Attack);
        }
        if (enemy._isAggro == false)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Idle);
        }
    }

    public override void PhysicsUpdate() { }
}
