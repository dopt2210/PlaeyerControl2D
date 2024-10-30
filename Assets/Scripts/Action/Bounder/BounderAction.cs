public class BounderAction : Action
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
