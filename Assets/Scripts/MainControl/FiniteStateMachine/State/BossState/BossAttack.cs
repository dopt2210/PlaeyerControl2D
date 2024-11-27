using UnityEngine;

public class BossAttack : BossState
{
    private float attackTimer;
    private const float attackTime = 2f;
    public BossAttack(Boss boss) : base(boss, "Attack", Boss.BossStateEnum.Attack)
    {
    }

    public override void AnimationTriggerEvent(Boss.BossStateEnum stateEnum) { }

    public override void EnterState()
    {
        attackTimer = 0f;
        boss.SetAnimation(anim, true);
        PerformRandomAttack();

    }

    public override void ExitState()
    {
        boss.SetAnimation(anim, false);
    }

    public override void LogicUpdate()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackTime)
        {
            boss.stateMachine.ChangeState(Boss.BossStateEnum.Idle);
        }
    }

    public override void PhysicsUpdate() { }
    private void PerformRandomAttack()
    {
        Vector3 playerPosition = GetPlayerPosition();

        for (int i = 0; i < boss.maxSpawnCount; i++)
        {
            int randomAttackType = Random.Range(0, boss.attackList.Count);
            GameObject attackObj = boss.GetFromPool(randomAttackType);

            if (attackObj != null)
            {
                Vector3 randomOffset = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
                attackObj.transform.position = playerPosition + randomOffset;
                attackObj.transform.rotation = Quaternion.identity;

                boss.StartCoroutine(DeactivateAfterTime(attackObj, 4f)); 
            }
        }
    }

    private System.Collections.IEnumerator DeactivateAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        boss.ReturnToPool(obj);
    }
    private Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player != null ? player.transform.position : Vector3.zero;
    }
}
