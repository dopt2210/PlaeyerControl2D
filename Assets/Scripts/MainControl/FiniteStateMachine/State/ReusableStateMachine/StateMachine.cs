using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<EState> where EState : Enum
{
    protected bool isChangeState;
    public Dictionary<EState, EnemyState<EState>> States = new Dictionary<EState, EnemyState<EState>>();
    public EnemyState<EState> currentState;
    public virtual void InitState(EnemyState<EState> state)
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
