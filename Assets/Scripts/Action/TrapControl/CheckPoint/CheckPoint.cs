using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Action
{
    public override void Act()
    {
        SaveCheckPoint();
    }

    private void SaveCheckPoint()
    {
        CheckPointCtrl.Instance.SetCheckPoint(triggerActionCtrl);
    }
}
