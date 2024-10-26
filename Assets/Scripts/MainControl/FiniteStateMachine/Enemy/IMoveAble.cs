using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveAble 
{
    Rigidbody2D _rb { get; set; }
    bool _isFacingRight {  get; set; }
    void MoveEnemy(Vector2 velocity);
    void SetFacingRight(Vector2 direction);
}
