using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    protected int moveX;
    protected bool JumpInput;
    
    public GroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBool) : base(player, playerStateMachine, playerData, animBool)
    {

    }

    public override void Checks ()
    {
        base.Checks();
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        moveX = PlayerCtrl.instance.MoveX;
        JumpInput = PlayerCtrl.instance.JumpDown;

        if (JumpInput)
        {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
