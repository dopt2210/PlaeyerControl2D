using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerState
{
    private bool _isGrounded;
    public AirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBool) : base(player, playerStateMachine, playerData, animBool)
    {
    }

    public override void Checks()
    {
        base.Checks();
        _isGrounded = player.OnGround;
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
        if (_isGrounded && Mathf.Abs(player._velocity.y) < 1e-6f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else
        {
            player._anim.SetFloat("xVel", player._velocity.x);
            player._anim.SetFloat("yVel", Mathf.Abs(player._velocity.y));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
