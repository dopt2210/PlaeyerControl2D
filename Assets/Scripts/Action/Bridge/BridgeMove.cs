using UnityEngine;

public class BridgeMove : Action
{
    BridgeCtrl _bridgeCtrl { get; set; }
    
    private void Start()
    {
        _bridgeCtrl = transform.parent.parent.GetComponent<BridgeCtrl>();
        
        
    }
    public override void Act()
    {
        _bridgeCtrl.MoveBridge = true;
        
    }
    public override void CancelAct()
    {
        _bridgeCtrl.MoveBridge = false;
        
    }

    
}
