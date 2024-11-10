public class BridgeMove : Action
{
    public override void Act()
    {
        BridgeCtrl.Instance.MoveBridge = true;
    }
    public override void CancelAct()
    {
        BridgeCtrl.Instance.MoveBridge = false;
    }

    
}
