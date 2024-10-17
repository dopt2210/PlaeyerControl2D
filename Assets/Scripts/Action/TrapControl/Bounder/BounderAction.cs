using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BounderAction : Action
{
    public TriggerActionCtrl triggerActionCtrl;

    private void Awake()
    {
        triggerActionCtrl = transform.parent.GetComponent<TriggerActionCtrl>();
    }
    public override void Act()
    {
        CheckBounder();
    }
    private void CheckBounder()
    {
        BouderCtrl.Instance.OutBouder();
    }
}
