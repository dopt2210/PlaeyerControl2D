using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHit : Action
{
    public override void Act()
    {
        CheckTrap();   
    }
    private void CheckTrap()
    {
        TrapCtrl.Instance.Damage(1, triggerActionCtrl.triggerAndAction.Keys);
    }
}
