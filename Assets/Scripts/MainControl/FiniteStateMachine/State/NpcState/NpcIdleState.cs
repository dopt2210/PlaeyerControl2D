using UnityEngine;

public class NpcIdleState : NpcState
{
    public NpcIdleState(Npc npc): base(npc, "Idle", Npc.NpcStateEnum.Idle)
    {

    }

    public override void AnimationTriggerEvent(Npc.NpcStateEnum npcStateEnum) { }

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
        if (npc.IsInteractable && npc._isTalkable)
        {
            npc.Interact();
        }
        else if (!npc._isTalkable)
        {
            npc.DisableInteract();
        }
    }

    public override void PhysicsUpdate() { }
}
