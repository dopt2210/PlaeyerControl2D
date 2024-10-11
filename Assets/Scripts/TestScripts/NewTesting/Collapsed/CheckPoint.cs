using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Action
{
    public StateCtrl StateCtrl;
    private void Start()
    {
        StateCtrl = transform.parent.GetComponent<StateCtrl>();
    }
    public override void Act()
    {
        this.SaveCheckPoint();
    }

    private void SaveCheckPoint()
    {
        CheckPointManager.instance.SetCheckPoint(StateCtrl);
    }
}
