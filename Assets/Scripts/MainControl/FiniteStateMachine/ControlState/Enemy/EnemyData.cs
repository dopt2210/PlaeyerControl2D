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
    public float RangeAggro = 10f;
    public float RangeAttack = 1f;
    
}
