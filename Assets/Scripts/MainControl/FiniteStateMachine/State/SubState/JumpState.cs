using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AbilityState
{
    public JumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBool) : base(player, playerStateMachine, playerData, animBool)
    {
    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void EnterState()
    {
        base.EnterState();
        player.HandleJumping();
        _isAblilityDone = true;
        //_isJumping = true;
    }

    public override void ExitState()
    {
        base.ExitState();
        //_isJumping = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(PlayerCtrl.instance.JumpDown)  OnJumpDown(); 
        if(PlayerCtrl.instance.JumpReleased) if (_isJumping) OnJumpReleased();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
