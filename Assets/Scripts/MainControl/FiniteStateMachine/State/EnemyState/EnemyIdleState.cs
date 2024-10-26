using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmyIdleState : EnemyState<Enemy.EnemyStateEnum>
{
    Enemy enemy;
    private float tagertPosition;
    private float direction;

    public EnenmyIdleState(Enemy enemy) : base(Enemy.EnemyStateEnum.Idle)
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
        tagertPosition = GetRandonPoint();
    }

    private float GetRandonPoint()
    {
        float randomPoint;
        do
        {
            randomPoint = UnityEngine.Random.Range(enemy.transform.position.x - enemy._randomRange,
                                                   enemy.transform.position.x + enemy._randomRange);
        } while (Mathf.Abs(randomPoint - enemy.transform.position.x) < 1f);

        return randomPoint;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(enemy._isAggro == true)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Chase);
        }
        #region move
        direction = (tagertPosition - enemy.transform.position.x);
        direction = Mathf.Sign(direction);

        enemy.HandleMove(new Vector2(direction * enemy._randomSpeed, enemy._rb.velocity.y));

        if (Mathf.Abs(enemy.transform.position.x - tagertPosition) < 0.1f)
        {
            tagertPosition = GetRandonPoint();
        }
        #endregion
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
