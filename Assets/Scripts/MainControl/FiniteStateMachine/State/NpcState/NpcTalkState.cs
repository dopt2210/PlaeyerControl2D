using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTalkState : NpcState
{

    public NpcTalkState(Npc npc) : base(npc, "Talk", Npc.NpcStateEnum.Talk)
    {

    }

    public override void AnimationTriggerEvent(Npc.NpcStateEnum stateEnum)
    {
        base.AnimationTriggerEvent(stateEnum);
    }

    public override void EnterState()
    {
        npc.SetAnimation(anim, true);
    }

    public override void ExitState()
    {
        npc.SetAnimation(anim, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
