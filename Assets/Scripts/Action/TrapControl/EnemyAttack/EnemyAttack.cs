using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : Action
{
    Enemy enemy;
    public override void Awake()
    {
        triggerActionCtrl = GetComponentInParent<TriggerActionCtrl>();
        enemy = GetComponentInParent<Enemy>();
    }
    public override void Act()
    {
        enemy.SetAttack(true);
    }
    public override void CancelAct()
    {
        enemy.SetAttack(false);
    }
}