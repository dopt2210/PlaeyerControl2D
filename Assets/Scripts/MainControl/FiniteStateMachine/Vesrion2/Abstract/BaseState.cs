using System;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum
{
    public BaseState(EState stateKey)
    {
        this.StateKey = stateKey;
    }
    public EState StateKey {  get; private set; }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public abstract EState GetNextState();
    public abstract void CheckState();
    public abstract void OnTriggerEnter2D(Collider2D collision);
    public abstract void OnTriggerExit2D(Collider2D collision);
    public abstract void OnTriggerStay2D(Collider2D collision);
}
