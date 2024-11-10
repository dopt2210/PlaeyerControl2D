using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : Action
{
    public override void Awake()
    {
        triggerActionCtrl = GetComponentInParent<TriggerActionCtrl>();
    }
    public override void Act()
    {
        Debug.Log("hit");
    }
    public override void CancelAct()
    {
        Debug.Log("hit");
    }
}
