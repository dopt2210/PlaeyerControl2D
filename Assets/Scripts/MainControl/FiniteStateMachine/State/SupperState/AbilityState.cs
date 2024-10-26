using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : PlayerState
{
    protected bool _isAblilityDone;
    protected bool _isGrounded, _isWallLeft, _isWallRight, _isJumping;
    protected float _timeJumpWasPressed, _timeLeftGround = float.MinValue;
    protected bool _isJumpCutoffApplied;
    protected int _jumpLeft;
    public AbilityState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBool) : base(player, playerStateMachine, playerData, animBool)
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
        _isAblilityDone = false;
    }

    public override void ExitState()
    {
        base.ExitState();
        _isAblilityDone = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_isAblilityDone)
        {
            if (_isGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.AirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void OnJumpDown()
    {
        _timeJumpWasPressed = playerData.JumpBufferTime;
    }
    public void OnJumpReleased()
    {
        if (player._rb.velocity.y > 0 && !_isJumpCutoffApplied)
            player._rb.velocity *= new Vector2(1, playerData.JumpCutOffMultipiler);
        _isJumpCutoffApplied = true;
    }
}
