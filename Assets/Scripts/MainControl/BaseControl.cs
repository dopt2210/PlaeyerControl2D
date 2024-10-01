using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseControl : InputCtrl
{
    public override float _MoveInput()
    {
        return 1f;
    }
    public override bool _JumpInput()
    {
        return true;
    }
}
