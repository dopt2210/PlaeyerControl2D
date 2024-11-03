public class NpcState : BaseState<Npc.NpcStateEnum>
{
    public Npc npc;
    public string anim;
    public float startTime {  get; set; }
    public NpcState(Npc npc, string anim, Npc.NpcStateEnum stateKey) : base(stateKey)
    {
        this.npc = npc;
        this.anim = anim;

    }

    public override void AnimationTriggerEvent(Npc.NpcStateEnum stateEnum) { }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() { }
}
