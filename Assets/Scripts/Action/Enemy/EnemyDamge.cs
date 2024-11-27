using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamge : Action
{
    Enemy enemy;
    public override void Awake()
    {
        triggerActionCtrl = GetComponentInParent<TriggerActionCtrl>();
        enemy = GetComponentInParent<Enemy>();
    }
    public override void Act()
    {
        KillPlayer.Instance.Damage(1, triggerActionCtrl.triggerAndAction.Keys);
        NoticeCtrl.Instance.SetTextWhenDie("Enemy");
    }
}
