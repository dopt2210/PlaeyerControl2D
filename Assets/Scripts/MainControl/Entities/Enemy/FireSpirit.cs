using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : Enemy
{
    [SerializeField] private List<TriggerWithPlayer> triggerWithPlayer;

    protected override void LoadRangeTriggers()
    {
        triggerWithPlayer[0].SetRadiusTrigger(enemyData.RangeAggro, enemyData.RangeAggroOffset);

    }
    protected override void LoadEnemy()
    {
        isFlyingObject = true;
        _rb.gravityScale = 0;
        _maxHP = 1;
        _animationTime = 10f;
    }
}
