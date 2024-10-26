using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : Action
{
    Enemy enemy;
    public override void Awake()
    {
        triggerActionCtrl = GetComponentInParent<TriggerActionCtrl>();
        enemy =  GetComponentInParent<Enemy>();
    }
    public override void Act()
    {
        enemy.SetAggro(true);
    }
    public override void CancelAct()
    {
        enemy.SetAggro(false);
    }
}
