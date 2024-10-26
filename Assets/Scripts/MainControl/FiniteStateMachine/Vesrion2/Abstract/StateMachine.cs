using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;
    protected float startTime;
    protected bool isChangeState = false;
    protected virtual void Start()
    {
        CurrentState.EnterState();
        startTime = Time.time;
    }
    protected virtual void Update()
    {
        EState nextStateKey = CurrentState.GetNextState();
        if (!isChangeState && nextStateKey.Equals(CurrentState.StateKey))  {
            CurrentState.UpdateState();
        }
        else if(!isChangeState)
        {
            ChangeState(nextStateKey);
        }
    }

    public void ChangeState(EState nextStateKey)
    {
        if (!States.ContainsKey(nextStateKey))
        {
            Debug.LogError("State not found: " + nextStateKey);
            return;
        }

        isChangeState = true;
        CurrentState.ExitState();
        CurrentState = States[nextStateKey];
        CurrentState.EnterState();
        isChangeState = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CurrentState.OnTriggerEnter2D(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        CurrentState?.OnTriggerExit2D(other); 
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        CurrentState.OnTriggerStay2D(other);
    }

}
