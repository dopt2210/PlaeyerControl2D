using UnityEngine;

public class EnemyShotState : EnemyState
{
    GameObject player;
    private float timer;
    private float timeDelay = 1f;
    public EnemyShotState(Enemy enemy) : base(enemy, "Shot", Enemy.EnemyStateEnum.Shot)
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            ArcherShoot();
        }
        timer += Time.deltaTime;
        if (enemy._isShot == false)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Chase);
        }
        else if (enemy._isShot && enemy._isAttack)
        {
            enemy.stateMachine.ChangeState(Enemy.EnemyStateEnum.Attack);
        }
    }

    public override void PhysicsUpdate() { }
    void ArcherShoot()
    {
        
        if (enemy is Archer archer)
        {
            Vector2 direction = (player.transform.position - archer.transform.position).normalized;

            Vector2 spawnPosition = archer.shootPoint != null ? archer.shootPoint.position : enemy.transform.position;

            Rigidbody2D arrow = GameObject.Instantiate(archer.arrowPrefab, spawnPosition, Quaternion.identity);

            arrow.velocity = direction * archer.arrowSpeed;

            archer.DestroyArrow(arrow.gameObject, 2f);
        }
        

    }
}
