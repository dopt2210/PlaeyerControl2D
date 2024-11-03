public class EnemyState : BaseState<Enemy.EnemyStateEnum>
{
    public Enemy enemy;
    public string anim;
    public float startTime {  get; set; }
    public EnemyState(Enemy enemy, string anim, Enemy.EnemyStateEnum stateKey) : base(stateKey)
    {
        this.enemy = enemy;
        this.anim = anim;
        
    }

    public override void AnimationTriggerEvent(Enemy.EnemyStateEnum stateEnum) { }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void LogicUpdate() { }

    public override void PhysicsUpdate() { }
}
