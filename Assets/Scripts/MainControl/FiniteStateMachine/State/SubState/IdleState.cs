using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBool) : base(player, playerStateMachine, playerData, animBool)
    {

    }

    public override void Checks()
    {
        base.Checks();
    }

    public override void EnterState()
    {
        base.EnterState();
        player._rb.velocity = Vector2.zero;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (moveX != 0) stateMachine.ChangeState(player.RunState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.HanldleMove(1);
    }
}
