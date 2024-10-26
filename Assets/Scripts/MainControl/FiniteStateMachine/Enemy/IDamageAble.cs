using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble 
{
    float _maxHP {  get; set; }
    float _currentHP {  get; set; }
    void Damage(float dmg);
    void Died();
}
