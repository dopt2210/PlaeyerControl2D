public class DeadZone : Action
{
    public override void Act()
    {
        CheckBounder();
    }
    private void CheckBounder()
    {
        KillPlayer.Instance.Damage(1);
        NoticeCtrl.Instance.SetTextWhenDie("Fall down");
    }
}
