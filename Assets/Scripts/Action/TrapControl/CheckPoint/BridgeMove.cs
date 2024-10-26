using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMove : Action
{
    public override void Act()
    {
        MovingBridgeCtrl.Instance.MoveBridge = true;
    }
    public override void CancelAct()
    {
        MovingBridgeCtrl.Instance.MoveBridge = false;
    }

    
}
