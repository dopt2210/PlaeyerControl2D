using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Action
{
    public TriggerActionCtrl triggerActionCtrl;
    private void Start()
    {
        triggerActionCtrl = transform.parent.GetComponent<TriggerActionCtrl>();
    }
    public override void Act()
    {
        SaveCheckPoint();
    }

    private void SaveCheckPoint()
    {
        CheckPointCtrl.Instance.SetCheckPoint(triggerActionCtrl);
    }
}
