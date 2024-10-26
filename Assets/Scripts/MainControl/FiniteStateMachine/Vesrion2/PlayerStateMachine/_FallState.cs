using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _FallState : _AbilityState
{
    private _PlayerStateMachine _player;

    public _FallState(_PlayerStateMachine player)
        : base(_PlayerStateMachine.PlayerStateEnum.Fall)
    {
        _player = player;
    }
}
