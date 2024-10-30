using UnityEngine;

public class Trigger : MonoBehaviour
{
	public TriggerActionCtrl triggerActionCtrl;
    
    protected virtual void Awake()
    {
        triggerActionCtrl = transform.GetComponentInParent<TriggerActionCtrl>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerActionCtrl.triggerAndAction.TryGetValue(this, out Action action))
        {
            action.Act();
        }

    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerActionCtrl.triggerAndAction.TryGetValue(this, out Action action))
        {
            action.CancelAct();
        }
    }
}
