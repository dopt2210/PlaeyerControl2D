using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : Enemy
{
    [SerializeField] private List<TriggerWithPlayer> triggerWithPlayer;
    protected override void LoadRangeTriggers()
    {
        triggerWithPlayer[0].SetRadiusTrigger(enemyData.RangeAggro, enemyData.RangeAggroOffset);
        triggerWithPlayer[1].SetRadiusTrigger(enemyData.RangeAttack, enemyData.RangeAttackOffset);

    }
}
