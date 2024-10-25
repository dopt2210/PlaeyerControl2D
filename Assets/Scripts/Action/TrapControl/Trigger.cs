using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TriggerActionCtrl triggerActionCtrl;

    public virtual void Awake()
    {
        triggerActionCtrl = transform.GetComponentInParent<TriggerActionCtrl>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var action in triggerActionCtrl.actions)
            action.Act();
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var action in triggerActionCtrl.actions)
            action.CancelAct();
    }
}
