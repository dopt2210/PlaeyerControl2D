using UnityEngine;

public class BossAttack : BossState
{
    private float attackTimer;
    private float attackInterval;
    public BossAttack(Boss boss) : base(boss, "Attack", Boss.BossStateEnum.Attack)
    {
    }

    public override void AnimationTriggerEvent(Boss.BossStateEnum stateEnum) { }

    public override void EnterState()
    {
        attackInterval = Mathf.Max(0.5f, boss._animationTime); 
        attackTimer = 0f;

        boss.SetAnimation(anim, true);
    }

    public override void ExitState()
    {
        boss.SetAnimation(anim, false);
    }

    public override void LogicUpdate()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackInterval)
        {
            PerformRandomAttack();
            attackTimer = 0f;

            boss._animationTime = Mathf.Max(0.2f, boss._animationTime - 0.05f); 
        }

        if (attackTimer >= 3f) 
        {
            boss.stateMachine.ChangeState(Boss.BossStateEnum.Idle);
        }
    }

    public override void PhysicsUpdate() { }
    private void PerformRandomAttack()
    {
        int randomAttackType = Random.Range(0, 4); 
        Vector3 playerPosition = GetPlayerPosition();

        GameObject attackObj = boss.GetFromPool(randomAttackType);
        if (attackObj != null)
        {
            attackObj.transform.position = playerPosition + new Vector3(0, -2, 0);
            attackObj.transform.rotation = Quaternion.identity;

            boss.StartCoroutine(DeactivateAfterTime(attackObj, 4f)); 
        }
    }
    private void SpawnAttackPrefab(GameObject prefab, Vector3 position)
    {
        if (prefab != null)
        {
            GameObject.Instantiate(prefab, position, Quaternion.identity);
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
