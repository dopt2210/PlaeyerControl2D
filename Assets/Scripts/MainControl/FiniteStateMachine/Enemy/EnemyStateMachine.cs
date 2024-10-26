using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class EnemyStateMachine<EState> where EState : Enum
{
    protected bool isChangeState;
    public Dictionary<EState, EnemyState<EState>> States = new Dictionary<EState, EnemyState<EState>>();
    public EnemyState<EState> currentState;
    float startTime;
    public virtual void InitState(EnemyState<EState> state)
    {
        currentState = state;
        currentState.EnterState();
        startTime = Time.time;
    }
    public virtual void ChangeState(EState state)
    {
        if (!States.ContainsKey(state))
        {
            Debug.LogError("State not found: " + state);
            return;
        }
        isChangeState = true;
        currentState.ExitState();
        currentState = States[state];
        currentState.EnterState();
        isChangeState = false;
    }
}
