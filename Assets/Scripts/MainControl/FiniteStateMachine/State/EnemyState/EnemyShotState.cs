using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyShotState : EnemyState
{
    Transform player;
    private float timer;
    private float timeDelay = 1f;
    public EnemyShotState(Enemy enemy) : base(enemy, "Shot", Enemy.EnemyStateEnum.Shot)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum stateEnum) { }

    public override void EnterState() { enemy.SetAnimation(anim, true); }

    public override void ExitState() { enemy.SetAnimation(anim, false); }

    public override void LogicUpdate()
    {
        enemy.HandleMove(0);
        if (timer > timeDelay)
        {
            timer = 0;
            Vector2 direction = (player.transform.position - enemy.transform.position).normalized;
            Rigidbody2D arrow = GameObject.Instantiate(enemy.arrowPrefab, enemy.transform.position, Quaternion.identity);
            arrow.velocity = direction * 10f;

            enemy.DestroyArrow(arrow, 2f);
        }
        timer += Time.deltaTime;
        if (enemy._isShot == false)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Chase);
        }
    }

    public override void PhysicsUpdate() { }
}
