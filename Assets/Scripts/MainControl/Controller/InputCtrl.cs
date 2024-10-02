using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputCtrl : ScriptableObject
{
    public abstract Vector2 _MoveInput();
    public abstract bool _JumpInput();
    public abstract bool _JumpHeldInput();
    public abstract bool _ClimbInput();
    public abstract bool _DashInput();
}
