using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GroundedState : BaseState<_PlayerStateMachine.PlayerStateEnum>
{
    public _GroundedState(_PlayerStateMachine.PlayerStateEnum stateKey) : base(stateKey)
    {

    }

    public override void CheckState()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override _PlayerStateMachine.PlayerStateEnum GetNextState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerStay2D(Collider2D collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
