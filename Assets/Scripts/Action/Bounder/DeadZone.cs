public class DeadZone : Action
{
    public override void Act()
    {
        CheckBounder();
    }
    private void CheckBounder()
    {
        BouderCtrl.Instance.Damage(1);
    }
}
