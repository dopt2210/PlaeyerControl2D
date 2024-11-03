using UnityEngine;

public class NpcTalk : Action
{
    Npc npc;
    public override void Awake()
    {
        base.Awake();
        npc = GetComponentInParent<Npc>();
    }
    public override void Act()
    {
        npc.SetRangeInteract(true);
    }
    public override void CancelAct()
    {
        npc.SetRangeInteract(false);
    }

}
