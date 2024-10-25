using UnityEngine;

public class Trigger : MonoBehaviour
{
    public TriggerActionCtrl triggerActionCtrl;

    public virtual void Awake()
    {
        if (transform.parent.parent.gameObject.CompareTag("TerrainTrap"))
        {
            triggerActionCtrl = transform.parent.parent.GetComponent<TriggerActionCtrl>();
        }
        else
        {
            triggerActionCtrl = transform.parent.GetComponent<TriggerActionCtrl>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerActionCtrl.action.Act();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerActionCtrl.action.CancelAct();
    }
}
