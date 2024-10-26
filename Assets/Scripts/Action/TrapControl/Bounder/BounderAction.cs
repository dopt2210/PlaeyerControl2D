using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BounderAction : Action
{
    public override void Act()
    {
        CheckBounder();
    }
    private void CheckBounder()
    {
        BouderCtrl.Instance.OutBouder();
    }
}
