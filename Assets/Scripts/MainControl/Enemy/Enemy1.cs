using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    [SerializeField] private List<TriggerWithPlayer> triggerWithPlayer;
    protected override void Awake()
    {
        base.Awake();
        LoadRangeTriggers();
    }
    protected override void LoadRangeTriggers()
    {
        triggerWithPlayer[0].SetRadiusTrigger(enemyData.RangeAggro);
        triggerWithPlayer[1].SetRadiusTrigger(enemyData.RangeAttack);

    }
}
