using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : Action
{
    Enemy enemy;
    public override void Awake()
    {
        triggerActionCtrl = GetComponentInParent<TriggerActionCtrl>();
        enemy = GetComponentInParent<Enemy>();
    }
    public override void Act()
    {
        enemy.SetShot(true);
    }
    public override void CancelAct()
    {
        enemy.SetShot(false);
    }
}
