using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : Action
{
    Enemy enemy;
    public override void Awake()
    {
        triggerActionCtrl = transform.parent.GetComponentInParent<TriggerActionCtrl>();
        enemy =  GetComponentInParent<Enemy>();
    }
    public override void Act()
    {
        enemy.SetAggro(true);
        Debug.Log("In Range Aggro");
    }
    public override void CancelAct()
    {
        enemy.SetAggro(false);
        Debug.Log("Out Range Aggro");
    }
}
