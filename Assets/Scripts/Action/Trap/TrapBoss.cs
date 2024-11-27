using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBoss : TrapHit
{
    protected override void CheckTrap()
    {
        KillPlayer.Instance.KillByBoss(CameraCtrl.Instance.GetMainVirtual());
        NoticeCtrl.Instance.SetTextWhenDie("Trap of Boss");
    }
}
