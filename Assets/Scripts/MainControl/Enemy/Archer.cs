using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy
{
    [SerializeField] private List<TriggerWithPlayer> triggerWithPlayer;

    protected override void Awake()
    {
        base.Awake();
        LoadRangeTriggers();
    }
    protected override void LoadRangeTriggers()
    {
        triggerWithPlayer[0].SetRadiusTrigger(enemyData.RangeAggro*2, enemyData.RangeAggroOffset);
        triggerWithPlayer[1].SetRadiusTrigger(enemyData.RangeAttack, enemyData.RangeAttackOffset);
        triggerWithPlayer[2].SetRadiusTrigger(enemyData.RangeShot, enemyData.RangeShotOffset);
    }
}
