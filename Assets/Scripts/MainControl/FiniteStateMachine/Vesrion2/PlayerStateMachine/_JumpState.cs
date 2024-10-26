using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class _JumpState : _AbilityState
{
    private _PlayerStateMachine _player;
    public _JumpState(_PlayerStateMachine player)
        : base(_PlayerStateMachine.PlayerStateEnum.Jump)
    {
        _player = player;
    }
    public override void EnterState()
    {
        Debug.Log("Entering Jump State");
        _player._animator.SetBool("Jump", true);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Jump State");
        _player._animator.SetBool("Jump", false);
    }

    public override void UpdateState()
    {
        CheckState();
        if(PlayerCtrl.instance.JumpDown) _player.OnJumpDown();
        if (PlayerCtrl.instance.JumpReleased) _player.OnJumpReleased();

    }

    public override _PlayerStateMachine.PlayerStateEnum GetNextState()
    {
        if (_player.OnGround)
        {
            return _PlayerStateMachine.PlayerStateEnum.Idle; 
        }
        else if (_player._rigidbody2d.velocity.y < 0)  
        {
            return _PlayerStateMachine.PlayerStateEnum.Fall;  
        }

        return _PlayerStateMachine.PlayerStateEnum.Jump;  
    }
    public override void CheckState()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
    }

}
