using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmyIdleState : EnemyState
{
    private float tagertPosition;

    public EnenmyIdleState(Enemy enemy) : base(enemy, "Idle", Enemy.EnemyStateEnum.Idle)
    {

    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum enemyStateEnum) { }

    public override void EnterState()
    {
        enemy.SetAnimation(anim, true);
        tagertPosition = GetRandonPoint();
    }

    public override void ExitState() { enemy.SetAnimation(anim, false); }

    public override void LogicUpdate()
    {
        if(enemy._isAggro == true)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Chase);
        }
        
    }

    public override void PhysicsUpdate()
    {
        #region move
        float moveDirection = 
            Mathf.Sign(tagertPosition - enemy.transform.position.x);
        float distanceToTarget = 
            Mathf.Abs(tagertPosition - enemy.transform.position.x);

        enemy.HandleMove(moveDirection * enemy.enemyData.SpeedNormal);

        if (distanceToTarget < 0.1f)
        {
            tagertPosition = GetRandonPoint();
        }
        #endregion
    }
    private float GetRandonPoint()
    {
        float randomPoint;
        do
        {
            randomPoint = UnityEngine.Random.Range(enemy.transform.position.x - enemy.enemyData.RangeMove,
                                                   enemy.transform.position.x + enemy.enemyData.RangeMove);
        } while (Mathf.Abs(randomPoint - enemy.transform.position.x) < 1f);

        return randomPoint;
    }
}
