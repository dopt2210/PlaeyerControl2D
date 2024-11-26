public class BossDie : BossState
{
    public BossDie(Boss boss) : base(boss, "Die", Boss.BossStateEnum.Die)
    {
    }

    public override void AnimationTriggerEvent(Boss.BossStateEnum stateEnum) { }

    public override void EnterState() { boss.SetAnimation(anim, true); }

    public override void ExitState() { boss.SetAnimation(anim, false); }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() { }
}
