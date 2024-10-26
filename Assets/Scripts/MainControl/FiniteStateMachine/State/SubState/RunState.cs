using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GroundedState
{
    public RunState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBool) : base(player, playerStateMachine, playerData, animBool)
    {

    }

    public override void Checks()
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
        if (moveX == 0) stateMachine.ChangeState(player.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.HanldleMove(1);
        
    }
}
