using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHit : Action
{
    public override void Act()
    {
        CheckTrap();   
    }
    protected virtual void CheckTrap()
    {
        KillPlayer.Instance.Damage(1, triggerActionCtrl.triggerAndAction.Keys);
        NoticeCtrl.Instance.SetTextWhenDie("Trap");
    }
}
