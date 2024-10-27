using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<EState> where EState : Enum
{
    protected bool isChangeState;
    public Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    public BaseState<EState> currentState;
    public virtual void InitState(BaseState<EState> state)
    {
        currentState = state;
        currentState.EnterState();
    }
    public virtual void ChangeState(EState state)
    {
        if (!States.ContainsKey(state)) return;
        isChangeState = true;
        currentState.ExitState();
        currentState = States[state];
        currentState.EnterState();
        isChangeState = false;
    }
}
