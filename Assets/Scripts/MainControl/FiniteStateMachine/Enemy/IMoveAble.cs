using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveAble 
{
    bool _isFacingRight {  get; set; }
    void HandleMove(Vector2 velocity);
    void SetFacingRight(Vector2 direction);
}
