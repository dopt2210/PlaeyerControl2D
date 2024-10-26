using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Action
{
    Enemy enemy;
    public override void Awake()
    {
        triggerActionCtrl = transform.parent.GetComponentInParent<TriggerActionCtrl>();
        enemy = GetComponentInParent<Enemy>();
    }
    public override void Act()
    {
        enemy.SetAttack(true);
        Debug.Log("In Range Attack");
    }
    public override void CancelAct()
    {
        enemy.SetAttack(false);
        Debug.Log("Out Range Attack");
    }
}