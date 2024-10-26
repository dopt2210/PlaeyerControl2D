using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _IdleState : _GroundedState
{
    private _PlayerStateMachine _player;

    public _IdleState(_PlayerStateMachine player) 
        : base(_PlayerStateMachine.PlayerStateEnum.Idle)
    {
        _player = player;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Idle State");
        _player._animator.SetBool("Idle", true); 
        _player._rigidbody2d.velocity = Vector2.zero; 
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Idle State");
        _player._animator.SetBool("Idle", false);
    }

    public override void UpdateState()
    {
        CheckState();
        
    }

    public override _PlayerStateMachine.PlayerStateEnum GetNextState()
    {
        if (Mathf.Abs(PlayerCtrl.instance.MoveX) != 0)
        {
            return _PlayerStateMachine.PlayerStateEnum.Run;
        }
        return _PlayerStateMachine.PlayerStateEnum.Idle;
        
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
