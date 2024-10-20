using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAction : Action
{
    public TriggerActionCtrl triggerActionCtrl;

    private void Awake()
    {
        triggerActionCtrl = transform.parent.GetComponent<TriggerActionCtrl>();
    }
    public override void Act()
    {
        CheckTrap();   
    }
    private void CheckTrap()
    {
        TrapCtrl.Instance.HitTrap(1, triggerActionCtrl.triggers);
    }
}
