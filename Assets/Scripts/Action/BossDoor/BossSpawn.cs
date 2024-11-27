using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : Action
{
    public override void Act()
    {
        TempoBoss.Instance.InitBoss();
        TempoBoss.Instance.CloseBossDoor();
    }

}
