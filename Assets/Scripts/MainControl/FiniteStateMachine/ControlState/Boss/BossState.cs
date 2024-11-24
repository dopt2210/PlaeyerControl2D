public class BossState : BaseState<Boss.BossStateEnum>
{
    public Boss boss;
    public string anim;
    public BossState(Boss boss, string anim, Boss.BossStateEnum stateKey) : base(stateKey)
    {
        this.anim = anim;
        this.boss = boss;
    }

    public override void AnimationTriggerEvent(Boss.BossStateEnum stateEnum) { }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() { }
}
