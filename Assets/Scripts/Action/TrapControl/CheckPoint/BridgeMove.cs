using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMove : Action
{
    public TriggerActionCtrl triggerActionCtrl;
    private void Start()
    {
        triggerActionCtrl = transform.parent.GetComponent<TriggerActionCtrl>();
        MoveByPoint.Instance.LoadPoint();
    }
    private void Update()
    {
        if(!MoveBridge)
            MoveByPoint.Instance.EarlyEndPointMoving();
        else
            MoveByPoint.Instance.PointMoving();

    }
    private void FixedUpdate()
    {
        MoveByPoint.Instance.NextPointCal();
    }
    public override void Act()
    {
        MoveBridge = true;
    }
    public override void CancelAct()
    {
        MoveBridge = false;
    }

    private bool MoveBridge;
    
}
