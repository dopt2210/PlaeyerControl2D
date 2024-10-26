using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class _RunState : _GroundedState
{
    private _PlayerStateMachine _player;
    public _RunState(_PlayerStateMachine player) 
        : base(_PlayerStateMachine.PlayerStateEnum.Run)
    {
        _player = player;
    }

    public override void EnterState()
    {
        Debug.Log("Entering Run State");
        _player._animator.SetBool("Run", true);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Run State");
        _player._animator.SetBool("Run", false);
    }

    public override void UpdateState()
    {
        CheckState();
    }

    public override _PlayerStateMachine.PlayerStateEnum GetNextState()
    {
        if (Mathf.Abs(PlayerCtrl.instance.MoveX) == 0)
        {
            return _PlayerStateMachine.PlayerStateEnum.Idle;
        }
        return _PlayerStateMachine.PlayerStateEnum.Run;
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
