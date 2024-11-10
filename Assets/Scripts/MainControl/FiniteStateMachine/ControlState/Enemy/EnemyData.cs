using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy Stats")]
public class EnemyData : ScriptableObject
{
    [Header("Speed")]
    public float SpeedNormal = 1f;
    public float SpeedChase = 2f;
    [Header("Range Detect")]
    public float RangeMove = 5f;

    public Vector2 RangeAggro = new Vector2(10f,10f);
    public Vector2 RangeAggroOffset = new Vector2(0, 0);

    public Vector2 RangeAttack = new Vector2(1f, 1f);
    public Vector2 RangeAttackOffset = new Vector2(0.5f, -1f);

    public Vector2 RangeShot = new Vector2(10, 1.2f);
    public Vector2 RangeShotOffset = new Vector2(0f, -0.7f);
}
