using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMove : Action
{
    public TriggerActionCtrl triggerActionCtrl;
    private void Start()
    {
        triggerActionCtrl = transform.parent.GetComponent<TriggerActionCtrl>();
    }

    public override void Act()
    {
        MovingBridgeCtrl.Instance.MoveBridge = true;
    }
    public override void CancelAct()
    {
        MovingBridgeCtrl.Instance.MoveBridge = false;
    }

    
}
