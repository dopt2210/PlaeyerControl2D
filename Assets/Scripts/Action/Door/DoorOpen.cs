public class DoorOpen : Action
{
    public override void UpdateAct()
    {
        DoorCtrl.Instance.Interact();
    }
    

}
